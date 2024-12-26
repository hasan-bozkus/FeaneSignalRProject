using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SignalRWebUI.Dtos.MailDtos;
using System.IO;
namespace SignalRWebUI.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateMailDto createMailDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("SignalR Rezervasyon", "hhasanbozus0147@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody=createMailDto.Body;

            mimeMessage.Subject = createMailDto.Subject;

            // Dosya ekleme kısmı
            if (createMailDto.Attachment != null && createMailDto.Attachment.Length > 0)
            {

                // Dosya içeriğini okuma
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    createMailDto.Attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                // MimePart oluşturma
                var attachmentPart = new MimePart
                {
                    Content = new MimeContent(new MemoryStream(fileBytes)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = createMailDto.Attachment.FileName
                };

                // MessageBody'ye ekleme
                bodyBuilder.Attachments.Add(attachmentPart);

            }
            mimeMessage.Body = bodyBuilder.ToMessageBody();


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("hhasanbozkus0147@gmail.com", "loqnnbwbxkyzfgbn");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return RedirectToAction("Index", "Category");
        }
    }
}
