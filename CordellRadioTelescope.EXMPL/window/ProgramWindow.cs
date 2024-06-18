﻿using Colorify;
using Colorify.UI;
using Figgle;


namespace CordellRadioTelescope.EXMPL.window {
    public class ProgramWindow {
        public ProgramWindow(ConsoleColor backGround, Format colorFormat) {
            _backGround  = backGround;
            _colorFormat = colorFormat;

            _colorFormat.ResetColor();
            _colorFormat.Clear();

            ClearScreen(backGround);

        }

        private ConsoleColor _backGround { get; set; } 
        private Format _colorFormat { get; set; }

        public void DrawMainWindow() {

            // Info data

            var options = new OptionGenerator(new List<string> {
                    "0) RTL-SDR setup", "1) XY setup", "2) Spectrum", "3) Waterfall", "4) Movement", "5) Summary"
                },
                new List<string> {
                    "This window will give interface for setting RTL-SDR COM-port. In different OS COM-ports named differend. You should do this as first step.",
                    "XY setup needed when you have navigation system of two or more motors. There you can choose COM-port, power and other stuff.",
                    "Spectrum analyzer window show spectrum in real time. RTL-SDR send data by serial port to Cordell RSA, then Cordell RSA draw graphs of spectrum.",
                    "Waterfall window is a second part of spectrum window. Every second whis window draws line of spectrum, store, and move it down. With this window you can find blinking sighnal.",
                    "XY movement window for working with your navigation system. Don`t forget check XY setup window.",
                    "Summary window includes data about authors and simple guide how to create your own radiotelescope"
                },
                new List<Action> {
                    DrawRTLsetupWindow, DrawXYsetupWindow, DrawSpectrumWindow, DrawWaterfallWindow, DrawMovementWindow, DrawSummaryWindow
                }
            );

            // Window loop

            while (true) {
                _colorFormat.AlignCenter("Cordell RSA program. Credits: j1sk1ss", Colors.bgSuccess);

                Console.WriteLine(FiggleFonts.Standard.Render("Cordell RSA"));
                Console.WriteLine("\n\n\n\n\n\n");

                options.DrawOptions(_colorFormat);
                options.ReadInput(Console.ReadKey(false).Key);

                ClearScreen(ConsoleColor.White);
            }
        }

        #region [RTL setup]

        string comPort = "not set";
        string centralFreq = "1420";
        string sampleRate = "2";
        string tunerGain = "AGC";
        string agcMode = "Enabled";
        string maxBuffer = "524288";
        string dropSamples = "True";

        private void DrawRTLsetupWindow() {
            ClearScreen(ConsoleColor.White);

            var options = new OptionGenerator(new List<string> {
                    $"0) COM-port <{comPort}>", $"1) Center frequency <{centralFreq} mHz>", $"2) Sample rate <{sampleRate} mHz>",
                    $"3) Tuner gain mode <{tunerGain}>", $"4) AGC mode <{agcMode}>", $"5) Max async buffer size <{maxBuffer}>", $"6) Drop samples on full buffer <{dropSamples}>"
                },
                null,
                new List<Action> {
                    SetRTLPort, SetCentralFreq, SetSampleRate, SetTunerGate, SetAGCmode, SetMaxBuffer, SetDropSample
                }
            );

            while (true) {
                _colorFormat.AlignCenter("RTL setup", Colors.bgSuccess);

                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

                options.DrawOptions(_colorFormat);

                var answer = Console.ReadKey(false).Key;
                options.ReadInput(answer);

                if (answer == ConsoleKey.Enter) break;
                ClearScreen(ConsoleColor.White);
            }

            DrawRTLsetupWindow();
        }

        private void SetRTLPort() {
            Console.WriteLine("Type number of port: ");
            comPort = Console.ReadLine()!;
        }

        private void SetCentralFreq() {
            Console.WriteLine("Set central freq: ");
            centralFreq = Console.ReadLine()!;
        }

        private void SetSampleRate() {
            Console.WriteLine("Set sample rate: ");
            sampleRate = Console.ReadLine()!;
        }

        private void SetTunerGate() {
            Console.WriteLine("New tuner gate: ");
            tunerGain = Console.ReadLine()!;
        }

        private void SetAGCmode() {
            Console.WriteLine("New AGC mode: ");
            agcMode = Console.ReadLine()!;
        }

        private void SetMaxBuffer() {
            Console.WriteLine("Set buffer size: ");
            maxBuffer = Console.ReadLine()!;
        }

        private void SetDropSample() {
            Console.WriteLine("Drop samples (True / False): ");
            dropSamples = Console.ReadLine()!;
        }

        #endregion

        private void DrawXYsetupWindow() {
            _colorFormat.AlignCenter("XY setup", Colors.bgSuccess);

            // COM port
            // ?
        }

        private void DrawSpectrumWindow() {
            _colorFormat.AlignCenter("Radio Spectrum Analyzer", Colors.bgSuccess);

            // Spectrum 
        }

        private void DrawWaterfallWindow() {
            _colorFormat.AlignCenter("Waterfall Analyzer", Colors.bgSuccess);

            // Waterfall
        }

        private void DrawMovementWindow() {
            _colorFormat.AlignCenter("Movement manager", Colors.bgSuccess);

            // Navigation controls
        }

        private void DrawSummaryWindow() {
            _colorFormat.AlignCenter("Summary", Colors.bgSuccess);

            // Authors, license. Mini guide for radio telecscope
        }

        private void ClearScreen(ConsoleColor color) {
            Console.BackgroundColor = color;
            Console.SetCursorPosition(0, 0);
            Console.Clear();
        }
    }
}
