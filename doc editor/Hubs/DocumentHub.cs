using doc_editor.Models;

namespace doc_editor.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class DocumentHub : Hub
    {
        public static string DocumentContent = "";

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected:");

            await Clients.Caller.SendAsync("ReceiveDocumentDelta", 0, DocumentContent, "insert");
        }
        public  async Task UpdateDocumentDelta(string username,int position, string change, string type)
        {
            if (type == "insert")
            {
                Console.WriteLine($"server Insert:{change}");
                DocumentContent = DocumentContent.Insert(position, change);
            }
            else if (type == "delete")
            {
                DocumentContent = DocumentContent.Remove(position, 1);
                Console.WriteLine($"server Remove:{change}");
            }


            DBhandler.LogUpdate(username, position, change, type);

            await Clients.Others.SendAsync("ReceiveDocumentDelta", position, change, type);
        }

    }
}
