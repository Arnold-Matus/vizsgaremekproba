using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib.Mpeg;

namespace suliradio
{
    public class Lejatszo
    {
        private WaveOutEvent output;
        private AudioFileReader audio;
        private FadeInOutSampleProvider fade;
        public bool IsPlaying => output?.PlaybackState == PlaybackState.Playing;
        public void Play(string fajl)
        {
            Stop();

            audio = new AudioFileReader(fajl);
            fade = new FadeInOutSampleProvider(audio, true);

            fade.BeginFadeIn(1000);

            if (output == null)
            {
                output = new WaveOutEvent();
                output.PlaybackStopped += Output_PlaybackStopped;
            }

            output.Init(fade);
            output.Play();
        }

        public void Pause()
        {
            output?.Pause();
        }

        public void Resume()
        {
            output?.Play();
        }

        public void Stop()
        {
            if (output != null)
                output.Stop();

            audio?.Dispose();
            audio = null;
            fade = null;
        }

        public async Task FadeOutAndStop(int ms = 1000)
        {
            if (fade != null)
            {
                fade.BeginFadeOut(ms);
                await Task.Delay(ms);
            }

            Stop();
        }

        public TimeSpan CurrentTime() {
            TimeSpan currentTime = audio==null? TimeSpan.Zero : audio.CurrentTime;
            return currentTime;
        }
        private void Output_PlaybackStopped(object sender, StoppedEventArgs e) //
        {
            // Itt tudod kezelni, ha vége a számnak //
            // pl.: következő szám indítása //
        }

        public void Dispose()
        {
            Stop();

            output?.Dispose();
            output = null;
        }
    }
}
