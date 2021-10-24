using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Presentation
{
    public class PageModel<AP> : PageModel // AP = Action Provider
    {
        public PageModel(IEnumerable<object> state, AP actionProvider, IRequest request, AppContentResponder appContentResponder) : this(actionProvider, request, appContentResponder)
        {
            ViewModel.State = state;
        }
        
        public PageModel(AP actionProvider, IRequest request, AppContentResponder appContentResponder) : this(request,
            appContentResponder)
        {
            ViewModel = new ViewModel<AP> {ActionProvider = actionProvider};
        }
        
        public PageModel(IRequest request, AppContentResponder appContentResponder) : base(request, appContentResponder)
        {
            ViewModel = new ViewModel<AP>();
        }
        
        public new ViewModel<AP> ViewModel { get; set; }
    }
    
    public class PageModel
    {
        public PageModel(IRequest request, AppContentResponder appContentResponder)
        {
            Name = request.Url.LocalPath;
            Request = request;
            AppContentResponder = appContentResponder;
            Application = new ApplicationModel(appContentResponder.AppConf);
            string absolutePath = request.Url.AbsolutePath;
            string extension = Path.GetExtension(absolutePath);
            string path = absolutePath.Truncate(extension.Length);
            Layout = AppContentResponder.GetLayoutModelForPath(path);
            ViewModel = new ViewModel();
        }
        
        public ApplicationModel Application { get; set; }
        
        public string Name { get; set; }

        public string Scripts => Layout?.ScriptTags ?? string.Empty;
        public string StyleSheets => Layout?.StyleSheetLinkTags ?? string.Empty;

        public LayoutModel Layout { get; set; }
        
        private ViewModel _viewModel;
        public ViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                _viewModel.Application = Application;
                _viewModel.Page = this;
            }
        }

        /// <summary>
        /// Returns the data passed to the renderer when the page is rendered to the client.
        /// </summary>
        /// <returns></returns>
        public virtual object TemplateData()
        {
            return ViewModel;
        }
        
        public IRequest Request { get; set; }
        public AppContentResponder AppContentResponder { get; set; }
    }
}
