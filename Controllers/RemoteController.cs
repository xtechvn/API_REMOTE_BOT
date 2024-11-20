﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API_REMOTE_BOT.Controllers
{
    [Route("api/remote")]
    public class RemoteController : ControllerBase
    {
        [HttpGet("start-job")]
        public async Task<ActionResult> remoteStart(string JobFilePath)
        {
            try
            {

                ProcessStartInfo startInfo = new ProcessStartInfo("C:/x_tech_project_cicd/job/consummer_sync_to_elasticsearch/JobSyncStoreToElasticSearch.exe")
                {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    Verb = "runas", // Chạy với quyền admin
                    WorkingDirectory = Path.GetDirectoryName("C:/x_tech_project_cicd/job/consummer_sync_to_elasticsearch/JobSyncStoreToElasticSearch.exe")
                };
                Process.Start(startInfo);


                return Ok(new
                {
                    status = 0,
                    msg = "Start success"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = 1,
                    msg = ex.ToString()
                });
            }
        }

        [HttpGet("stop-job")]
        public async Task<ActionResult> remoteStop(string JobFilePath)
        {
            try
            {
                // Dừng tiến trình dựa trên tên của file exe
                var processName = System.IO.Path.GetFileNameWithoutExtension(JobFilePath);
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                    process.WaitForExit(); // Đảm bảo tiến trình dừng hẳn trước khi tiếp tục
                }

                return Ok(new
                {
                    status = 0,
                    msg = "Start success"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = 1,
                    msg = ex.ToString()
                });
            }
        }
    }
}
