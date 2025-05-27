using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace VPN.Classes
{
    public class SpeedTester
    {
        public event Action<double> OnDownloadMeasured;
        public event Action<double> OnUploadMeasured;

        private readonly byte[] _uploadData = new byte[10 * 1024 * 1024];

        public async Task StartTestAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await StartDownloadTestAsync(token);
                    await StartUploadTestAsync(token);
                    await Task.Delay(2000, token);
                }
                catch (TaskCanceledException)
                {
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Speed test error: " + ex.Message);
                }
            }
        }

        public async Task StartDownloadTestAsync(CancellationToken token)
        {
            const int testDurationMs = 5000;
            string url = "http://77.239.97.41/100MB.zip";

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    var stopwatch = Stopwatch.StartNew();
                    long totalBytes = 0;

                    using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            Debug.WriteLine("Download failed: " + response.StatusCode);
                            OnDownloadMeasured?.Invoke(-1);
                            return;
                        }

                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            var buffer = new byte[8192];
                            int read;

                            do
                            {
                                read = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                                totalBytes += read;
                            } while (read > 0 && stopwatch.ElapsedMilliseconds < testDurationMs && !token.IsCancellationRequested);
                        }
                    }

                    stopwatch.Stop();
                    double seconds = stopwatch.Elapsed.TotalSeconds;
                    if (totalBytes == 0 || seconds == 0)
                    {
                        OnDownloadMeasured?.Invoke(0);
                        return;
                    }

                    double mbps = (totalBytes * 8.0) / (seconds * 1_048_576.0);
                    OnDownloadMeasured?.Invoke(mbps);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Download Error: " + ex.Message);
                OnDownloadMeasured?.Invoke(0);
            }
        }

        public async Task StartUploadTestAsync(CancellationToken token)
        {
            string url = "http://77.239.97.41:8080/upload";

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    var stopwatch = Stopwatch.StartNew();

                    using (var content = new StreamContent(new MemoryStream(_uploadData)))
                    {
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        var response = await client.PostAsync(url, content, token);

                        if (!response.IsSuccessStatusCode)
                        {
                            Debug.WriteLine("Upload failed: " + response.StatusCode);
                            OnUploadMeasured?.Invoke(0);
                            return;
                        }
                    }

                    stopwatch.Stop();
                    double seconds = stopwatch.Elapsed.TotalSeconds;
                    double mbps = (_uploadData.Length * 8.0) / (seconds * 1_048_576.0);
                    OnUploadMeasured?.Invoke(mbps);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Upload Error: " + ex.Message);
                OnUploadMeasured?.Invoke(0);
            }
        }
    }
}
