using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API_REMOTE_BOT.Controllers
{
    [Route("api/remote")]
    public class RemoteController : ControllerBase
    {
        [HttpGet("start-job")]
        public async Task<ActionResult> remoteStart(string job_name)
        {
            try
            {
                // Đường dẫn đến lệnh schtasks để chạy tác vụ
                string command = "schtasks";
                string arguments = "/Run /TN \""+ job_name + "\""; // Tên tác vụ đã tạo trong Task Scheduler

                // Thiết lập ProcessStartInfo để chạy lệnh schtasks
                ProcessStartInfo startInfo = new ProcessStartInfo(command)
                {
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Khởi chạy tiến trình
                Process.Start(startInfo);

                return Ok(new
                {
                    status = 0,
                    msg = "Start success !"
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
        public async Task<ActionResult> remoteStop(string job_name)
        {
            try
            {

                // Đường dẫn đến lệnh schtasks để dừng tác vụ
                string command = "schtasks";
                string arguments = "/End  /TN \"" + job_name + "\""; // Tên tác vụ đã tạo trong Task Scheduler

                // Thiết lập ProcessStartInfo để chạy lệnh schtasks
                ProcessStartInfo startInfo = new ProcessStartInfo(command)
                {
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Khởi chạy tiến trình
                Process process = Process.Start(startInfo);
                process.WaitForExit(); // Đợi tiến trình hoàn tất

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
