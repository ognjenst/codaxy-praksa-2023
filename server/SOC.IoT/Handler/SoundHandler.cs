using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Handler
{
    public class SoundRequest : IRequest<NoOutput>
    {
        [JsonProperty("filePath")]
        public string? FilePath { get; set; }
    }

    [OriginalName("IoT_play_sound")]
    public class SoundHandler : ITaskRequestHandler<SoundRequest, NoOutput>
    {
        private readonly ILogger<SoundHandler> _logger;

        public SoundHandler(ILogger<SoundHandler> logger)
        {
            _logger = logger;
        }

        public async Task<NoOutput> Handle(
            SoundRequest request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("Playing sound...");

            if (File.Exists(request.FilePath))
            {
                using (var audioFile = new AudioFileReader(request.FilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    // Wait for the playback to finish
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        await Task.Delay(100);
                    }
                }
                _logger.LogInformation("Music playback complete.");
            }
            else
            {
                _logger.LogError("Sound file not found.");
            }

            return new NoOutput();
        }
    }
}
