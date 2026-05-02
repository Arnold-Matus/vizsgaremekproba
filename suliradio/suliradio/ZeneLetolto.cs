using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suliradio
{
    internal class ZeneLetolto
    {
        private string ytDlpPath = "ytdlp/yt-dlp.exe";
        private string ffmpegPath = "ffmpeg/ffmpeg.exe";
        private static HashSet<string> varoLista = new HashSet<string>();


        public static async Task<string> Letoltes(string url, string mentesiUt)
        {
            string outputTemplate = $"{mentesiUt}\\%(title)s.%(ext)s";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "ytdlp/yt-dlp.exe",
                Arguments = $"-x --audio-format mp3 --embed-metadata --ffmpeg-location \"ffmpeg/bin/ffmpeg.exe\" -o \"{outputTemplate}\" {url}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = new Process())
            {
                proc.StartInfo = psi;
                proc.Start();

                string output = await proc.StandardOutput.ReadToEndAsync();
                string error = await proc.StandardError.ReadToEndAsync();

                await proc.WaitForExitAsync();

                if (proc.ExitCode != 0)
                    MessageBox.Show("Hiba történt a letöltés során: " + error, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new Exception("Hiba: " + error);
                else {
                    string cim = ZeneCimKiemeles(output);
                    string substr = new String(cim.Substring(cim.IndexOf("[ExtractAudio] Destination: ") + "[ExtractAudio] Destination: ".Length).TakeWhile(x => x != '\n').ToArray());
                    MessageBox.Show($"A zene letöltése sikeres!\n\'\n\"\ncim: {substr}\n", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.Instance.LetoltottZeneHozzaad(substr);

                }

                

                return output;
            }
        }
        public static async Task<bool> LetoltveEllenorzes(string mentesiUt)
        {
            await Task.Delay(3000);
            int fut = 7;
            while (fut < 0)
            {
                if (!File.Exists(mentesiUt))
                {
                    // Ha a fájl nem létezik, akkor várunk
                    await Task.Delay(3000);
                    fut -= 1;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ZeneMarLetoltve(string zeneNev, string zenek_mappa)
        {
            if (!Directory.Exists(zenek_mappa))
                return false;

            var fajlok = Directory.GetFiles(zenek_mappa, "*.mp3");
            return fajlok.Any(f => Path.GetFileNameWithoutExtension(f).Equals(zeneNev, StringComparison.OrdinalIgnoreCase));
        }

        public static bool VaroListanVan(string zeneNev)
        {
            return varoLista.Contains(zeneNev);
        }

        public static void VaroListabol(string zeneNev)
        {
            varoLista.Remove(zeneNev);
        }

        private static string ZeneCimKiemeles(string output)
        {
            var sorok = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var cim = sorok.FirstOrDefault(s => s.Contains("[ExtractAudio]")) ?? "Ismeretlen";
            return cim.Trim();
        }

    }

}
