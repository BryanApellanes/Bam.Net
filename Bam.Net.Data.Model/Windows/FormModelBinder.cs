/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using Bam.Net.Data.Model.Windows;

namespace Bam.Net.Data.Model.Windows
{
    public class FormModelBinder
    {
        public FormModelBinder(Control.ControlCollection controls, List<MenuStrip> menuStrips = null)
        {
            List<Control> tmp = new List<Control>();
            foreach (Control control in controls)
            {
                tmp.Add(control);
            }
            Controls = tmp;
            MenuStrips = menuStrips ?? new List<MenuStrip>();
        }

        public List<Control> Controls { get; set; }
        public List<MenuStrip> MenuStrips { get; set; }
        public Type ViewModelType
        {
            get
            {
                return ViewModel.GetType();
            }
        }
        public object ViewModel { get; set; }

        public void Bind()
        {
            ForEachTaggedControl(control =>
            {
                BindText(control);
                BindDataSource(control);
                BindEventHandlers(control);
            });
            
            BindEventHandlersForEachTaggedToolStripItem();
        }
        public void ForEachTaggedControl(Action<Control> action)
        {
            Queue<Control> controls = GetControlQueue();
            while (controls.Count > 0)
            {
                Control control = controls.Dequeue();
                if (control != null)
                {
                    EnqueueChildControls(controls, control);
                    object tag = control.Tag;
                    if (tag != null)
                    {
                        action(control);
                    }
                }
            }
        }

        public void BindEventHandlersForEachTaggedToolStripItem()
        {
            ForEachTaggedToolStripItem(item =>
            {
                BindEventHandlers(item);
            });
        }

        public void ForEachTaggedToolStripItem(Action<ToolStripMenuItem> action)
        {
            foreach (MenuStrip menuStrip in MenuStrips)
            {
                Queue<ToolStripMenuItem> itemQueue = new Queue<ToolStripMenuItem>();
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {   
                    itemQueue.Enqueue(item);
                }
                while (itemQueue.Count > 0)
                {
                    ToolStripMenuItem item = itemQueue.Dequeue();
                    if (item != null)
                    {
                        foreach (ToolStripItem subItem in item.DropDown.Items)
                        {
                            ToolStripMenuItem menuItem = subItem as ToolStripMenuItem;
                            if (menuItem != null)
                            {
                                itemQueue.Enqueue(menuItem);
                            }
                        }
                        action(item);
                    }
                }
            }
        }

        public void BindTextForEachTaggedControl()
        {
            ForEachTaggedControl(c =>
            {
                BindText(c);
            });
        }

        public void BindDataSourceForEachTaggedControl()
        {
            ForEachTaggedControl(c =>
            {
                BindDataSource(c);
            });
        }

        public void BindText(Control control)
        {
            if (control.Tag != null)
            {
                PropertyInfo property = ViewModelType.GetProperty(control.Tag.ToString());
                if (property != null)
                {
                    string value = property.GetValue(ViewModel) as string;
                    if (value != null)
                    {
                        SetText(control, value);
                    }
                }
            }
        }
        
        public void BindCheckedState(Control control)
        {
            if (control.Tag != null)
            {
                PropertyInfo valueProperty = ViewModelType.GetProperty(control.Tag.ToString());
                if (valueProperty != null)
                {
                    PropertyInfo checkStateProperty = control.GetType().GetProperty("CheckState");
                    if (checkStateProperty != null)
                    {
                        control.On("CheckStateChanged", (o, a) => valueProperty.SetValue(ViewModel, checkStateProperty.GetValue(control)));
                        object checkState = valueProperty.GetValue(ViewModel);
                        checkStateProperty.SetValue(control, checkState);
                    }
                }
            }
        }

        public void BindEventHandlers(Control control)
        {
            object tag = control.Tag;
            object target = control;
            BindEventHandlers(tag, target);
        }

        public void BindEventHandlers(ToolStripItem item)
        {
            object tag = item.Tag;
            object target = item;
            BindEventHandlers(tag, target);
        }

        private void BindEventHandlers(object tag, object target)
        {
            Type controlType = target.GetType();
            if (tag != null)
            {
                controlType.GetEvents().Each(eventInfo =>
                {
                    MethodInfo method = ViewModelType.GetMethod("{0}On{1}"._Format(tag.ToString(), eventInfo.Name));
                    if (method != null)
                    {
                        Delegate d = Delegate.CreateDelegate(eventInfo.EventHandlerType, ViewModel, method);
                        eventInfo.AddEventHandler(target, d);
                    }
                });
            }
        }

        public void BindDataSource(Control control)
        {
            ListControl listControl = control as ListControl;
            if (listControl != null)
            {
                object tag = listControl.Tag;
                if (tag != null)
                {
                    string tagString = tag.ToString();
                    PropertyInfo dataSrcProp = ViewModelType.GetProperty("{0}DataSource"._Format(tagString)) ?? ViewModelType.GetProperty("{0}List"._Format(tagString));
                    if (dataSrcProp != null)
                    {
                        listControl.DataSource = dataSrcProp.GetValue(ViewModel);
                    }
                }
            }
        }

        public void SetByTag(string tag, string text)
        {
            ForEachTaggedControl(c =>
            {
                if (c.Tag.Equals(tag))
                {
                    CheckBox cb = c as CheckBox;
                    if (cb != null)
                    {
                        cb.CheckState = (CheckState)Enum.Parse(typeof(CheckState), text);
                    }
                    else
                    {
                        SetText(c, text);
                    }
                }
            });
        }

        public void ForListView(ListView listView, Action<ListView> listViewAction)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke((Action)(() => ForListView(listView, listViewAction)));
            }
            else
            {
                listViewAction(listView);
            }
        }

        public void ForControl<T>(T control, Action<T> controlAction) where T: Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)(() => ForControl(control, controlAction)));
            }
            else
            {
                controlAction(control);
            }
        }

        public void ForControl(Control label, Action<Control> labelAction)
        {
            if (label.InvokeRequired)
            {
                label.Invoke((Action)(() => ForControl(label, labelAction)));
            }
            else
            {
                labelAction(label);
            }
        }

        public void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)(() => SetText(control, text)));
            }
            else
            {
                control.Text = text;
            }
        }

        public void AppendText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)(() => AppendText(control, text)));
            }
            else
            {
                control.Text += text;
            }
        }

        public void DisableButtons()
        {
            Disable<Button>();
        }

        public void EnableButtons()
        {
            Enable<Button>();
        }

        public void Disable<T>() where T : Control
        {
            ForEachTaggedControl((c) =>
            {
                T t = c as T;
                if (t != null)
                {
                    Disable(t);
                }
            });
        }
        public void Enable<T>() where T : Control
        {
            ForEachTaggedControl((c) =>
            {
                T t = c as T;
                if (t != null)
                {
                    Enable(t);
                }
            });
        }

        public void Disable(Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)(() => Disable(control)));
            }
            else
            {
                control.Enabled = false;
            }
        }

        public void Enable(Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)(() => Enable(control)));
            }
            else
            {
                control.Enabled = true;
            }
        }

        private static void EnqueueChildControls(Queue<Control> controls, Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl != null)
                {
                    controls.Enqueue(ctrl);
                }
            }
        }

        private Queue<Control> GetControlQueue()
        {
            Queue<Control> controls = new Queue<Control>();
            Controls.Each(control =>
            {
                controls.Enqueue(control);
            });
            return controls;
        }

    }
}
