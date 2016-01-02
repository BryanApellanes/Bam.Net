/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Testing;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using Midi;
using Bam.Net.CommandLine;

namespace CommandLineTests
{
    public class TestProgram : CommandLineTestInterface
    {
        // Add optional code here to be run before initialization/argument parsing.
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
        }

        /*
          * Methods addorned with the ConsoleAction attribute can be run
          * interactively from the command line while methods addorned with
          * the TestMethod attribute will be run automatically when the
          * compiled executable is run.  To run ConsoleAction methods use
          * the command line argument /i.
          * 
          * All methods addorned with ConsoleAction and TestMethod attributes 
          * must be static for the purposes of extending CommandLineTestInterface
          * or an exception will be thrown.
          * 
          */

        // To run ConsoleAction methods use the command line argument /i.        
       

        [ConsoleAction("Midi output devices")]
        public static void MusicStuff()
        {
            if (OutputDevice.InstalledDevices.Count == 0)
            {
                Out("No midi devices");
            }
            else
            {
                foreach (OutputDevice device in OutputDevice.InstalledDevices)
                {
                    Out(device.Name, ConsoleColor.Cyan);
                }
            }
        }

        static void PlayChordRun(OutputDevice outputDevice, Chord chord, int millisecondsBetween)
        {
            Pitch previousNote = (Pitch)(-1);
            for (Pitch pitch = Pitch.A0; pitch < Pitch.C8; ++pitch)
            {
                if (chord.Contains(pitch))
                {
                    if (previousNote != (Pitch)(-1))
                    {
                        outputDevice.SendNoteOff(Channel.Channel1, previousNote, 80);
                    }
                    outputDevice.SendNoteOn(Channel.Channel1, pitch, 80);
                    Thread.Sleep(millisecondsBetween);
                    previousNote = pitch;
                }
            }
            if (previousNote != (Pitch)(-1))
            {
                outputDevice.SendNoteOff(Channel.Channel1, previousNote, 80);
            }
        }


        [ConsoleAction("play stuff")]
        public static void PlayStuff()
        {
            // Prompt user to choose an output device (or if there is only one, use that one).
            OutputDevice outputDevice = OutputDevice.InstalledDevices[0];
            if (outputDevice == null)
            {
                Out("No output devices, so can't run this example.");                
                return;
            }
            outputDevice.Open();

            Console.WriteLine("Playing an arpeggiated C chord and then bending it down.");
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 0);
            outputDevice.SendPitchBend(Channel.Channel1, 8192);
            // Play C, E, G in half second intervals.
            outputDevice.SendNoteOn(Channel.Channel1, Pitch.C4, 80);
            Thread.Sleep(500);
            outputDevice.SendNoteOn(Channel.Channel1, Pitch.E4, 80);
            Thread.Sleep(500);
            outputDevice.SendNoteOn(Channel.Channel1, Pitch.G4, 80);
            Thread.Sleep(500);

            // Now apply the sustain pedal.
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 127);

            // Now release the C chord notes, but they should keep ringing because of the sustain
            // pedal.
            outputDevice.SendNoteOff(Channel.Channel1, Pitch.C4, 80);
            outputDevice.SendNoteOff(Channel.Channel1, Pitch.E4, 80);
            outputDevice.SendNoteOff(Channel.Channel1, Pitch.G4, 80);

            // Now bend the pitches down.
            for (int i = 0; i < 17; ++i)
            {
                outputDevice.SendPitchBend(Channel.Channel1, 8192 - i * 450);
                Thread.Sleep(200);
            }

            // Now release the sustain pedal, which should silence the notes, then center
            // the pitch bend again.
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 0);
            outputDevice.SendPitchBend(Channel.Channel1, 8192);

            Console.WriteLine("Playing the first two bars of Mary Had a Little Lamb...");
            Clock clock = new Clock(120);
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 0));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 1));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 1));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 2));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.C4, 80, 2));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.C4, 80, 3));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 3));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 4));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 4));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 5));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 5));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 6));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 6));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 7));
            clock.Start();
            Thread.Sleep(5000);
            clock.Stop();

            Console.WriteLine("Playing sustained chord runs up the keyboard...");
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 127);
            PlayChordRun(outputDevice, new Chord("C"), 100);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 0);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 127);
            PlayChordRun(outputDevice, new Chord("F"), 100);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 0);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 127);
            PlayChordRun(outputDevice, new Chord("G"), 100);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 0);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 127);
            PlayChordRun(outputDevice, new Chord("C"), 100);
            Thread.Sleep(2000);
            outputDevice.SendControlChange(Channel.Channel1, Control.SustainPedal, 0);

            // Close the output device.
            outputDevice.Close();

            // All done.
            Console.WriteLine();
        }

        // Defines QUERTY order so that the percussion sounds can map to the keyboard in
        // that order.
        private static ConsoleKey[] QwertyOrder = new ConsoleKey[] {
            ConsoleKey.Q, ConsoleKey.W, ConsoleKey.E, ConsoleKey.R, ConsoleKey.T, ConsoleKey.Y, 
            ConsoleKey.U, ConsoleKey.I, ConsoleKey.O, ConsoleKey.P, ConsoleKey.A, ConsoleKey.S, 
            ConsoleKey.D, ConsoleKey.F, ConsoleKey.G, ConsoleKey.H, ConsoleKey.J, ConsoleKey.K, 
            ConsoleKey.L, ConsoleKey.Z, ConsoleKey.X, ConsoleKey.C, ConsoleKey.V, ConsoleKey.B, 
            ConsoleKey.N, ConsoleKey.M 
        };

        [ConsoleAction("Percussion keys")]
        public static void PercussionKeys()
        {
            // Prompt the user to choose an output device (or if there is only one, use that one).
            OutputDevice outputDevice = OutputDevice.InstalledDevices[0];//ExampleUtil.ChooseOutputDeviceFromConsole();
            if (outputDevice == null)
            {
                Console.WriteLine("No output devices, so can't run this example.");
                return;
            }
            outputDevice.Open();

            // Generate two maps from console keys to percussion sounds: one for when alphabetic
            // keys are pressed and one for when they're pressed with shift.
            Dictionary<ConsoleKey, Percussion> unshiftedKeys =
                new Dictionary<ConsoleKey, Percussion>();
            Dictionary<ConsoleKey, Percussion> shiftedKeys =
                new Dictionary<ConsoleKey, Percussion>();
            for (int i = 0; i < 26; ++i)
            {
                unshiftedKeys[QwertyOrder[i]] = Percussion.BassDrum1 + i;
                if (i < 20)
                {
                    shiftedKeys[QwertyOrder[i]] = Percussion.BassDrum1 + 26 + i;
                }
            }

            Console.WriteLine("Press alphabetic keys (with and without SHIFT) to play MIDI " +
                "percussion sounds.");
            Console.WriteLine("Press Escape when finished.");
            Console.WriteLine();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    if (shiftedKeys.ContainsKey(keyInfo.Key))
                    {
                        Percussion note = shiftedKeys[keyInfo.Key];
                        Console.Write("\rNote {0} ({1})         ", (int)note, note.Name());
                        outputDevice.SendPercussion(note, 90);
                    }
                }
                else
                {
                    if (unshiftedKeys.ContainsKey(keyInfo.Key))
                    {
                        Percussion note = unshiftedKeys[keyInfo.Key];
                        Console.Write("\rNote {0} ({1})         ", (int)note, note.Name());
                        outputDevice.SendPercussion(note, 90);
                    }
                }
            }

            // Close the output device.
            outputDevice.Close();

            // All done.
        }

        [ConsoleAction("Mary had a little lamb")]
        public static void Mary()
        {
            OutputDevice outputDevice = OutputDevice.InstalledDevices[0];
            outputDevice.Open();
            Console.WriteLine("Playing the first two bars of Mary Had a Little Lamb...");
            Clock clock = new Clock(120);
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 0));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 1));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 1));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 2));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.C4, 80, 2));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.C4, 80, 3));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 3));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.D4, 80, 4));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 4));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 5));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 5));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 6));
            clock.Schedule(new NoteOnMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 6));
            clock.Schedule(new NoteOffMessage(outputDevice, Channel.Channel1, Pitch.E4, 80, 7));
            clock.Start();
            Thread.Sleep(5000);
            clock.Stop();

            outputDevice.Close();
        }

        #region do not modify
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }


        #endregion
    }
}
