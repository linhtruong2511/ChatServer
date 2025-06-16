using ClientApp.common.Class;
using ClientApp.common.Enum;
using ClientApp.utils;
using Newtonsoft.Json;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace ClientApp
{
    public class Controller
    {
        public Context Context { get; set; }
        public bool IsLoading { get; set; } = false;
        public Controller(Context Context)
        {
            this.Context = Context;
        }
        public void HandleReadingTask(MainForm MainForm)
        {
            Packet packet;
            PacketFile packetFile;
            MyFileInfo file;
            do
            {
                packet = NetworkUtils.Read(Context.Reader);
                switch (packet.Type)
                {
                    case PacketTypeEnum.NOTIFICATION:
                        if (Encoding.UTF8.GetString(packet.Data) != "end")
                            MessageBox.Show($"{Encoding.UTF8.GetString(packet.Data)}", "Notifcation");
                        if(Encoding.UTF8.GetString(packet.Data) == "Password change complete")
                        {
                            MainForm.Close();
                        }
                        break;
                    case PacketTypeEnum.DISCONNECT:
                        Application.Exit();
                        break;
                    case PacketTypeEnum.SENDMESSAGE:
                        if (packet.From == Context.DestinationId)
                        {
                            HistoryMessage hm = new HistoryMessage(packet.From, packet.createAt, Encoding.UTF8.GetString(packet.Data));
                            MainForm.ShowChatObject(hm, false);
                        }
                        break;
                    case PacketTypeEnum.HISTORYMESSAGES:
                            HistoryMessage historyMessage = new HistoryMessage(packet.From, packet.createAt, Encoding.UTF8.GetString(packet.Data));
                            Context.chatObjects.Add(historyMessage.CreateAt, historyMessage);
                            Context.count++;
                            Console.WriteLine(Context.count);
                            if (Context.count == Context.numberOfLoadChat)
                            {
                                LoadConservation(MainForm);
                            }
                        break;
                    case PacketTypeEnum.HISTORYFILE:

                        packetFile = (PacketFile)packet;
                            file = new MyFileInfo(packetFile.fileName, 
                                packetFile.Data, packetFile.From, 
                                packetFile.createAt);
                            Context.chatObjects.Add(file.CreateAt, file);
                            Context.count++;
                            if (Context.count == Context.numberOfLoadChat)
                            {
                                LoadConservation(MainForm);
                            }
                        break;
                    case PacketTypeEnum.SENDFILE:
                        packetFile = (PacketFile)packet;
                        file = new MyFileInfo(packetFile.fileName, packetFile.Data, packetFile.From, packetFile.createAt);
                        if (packetFile.From == Context.DestinationId)
                        {
                            Context.chatObjects.Add(file.CreateAt, file);
                            MainForm.ShowChatObject(file, false);
                        }
                        break;
                    case PacketTypeEnum.ADDUSERINFO:
                        MainForm.ShowUser(JsonConvert.DeserializeObject<UserInfo>(Encoding.UTF8.GetString(packet.Data)));
                        break;
                    case PacketTypeEnum.DELETEMESSAGE:
                        int index_message = Context.chatObjects.IndexOfKey(packet.createAt);
                        if (packet.From == Context.DestinationId && packet.To == Context.MyId && index_message != -1 && Context.chatObjects.Values[index_message] is HistoryMessage)
                        {
                            HistoryMessage hMessage = (HistoryMessage)Context.chatObjects.Values[index_message];
                            if (hMessage.Control.InvokeRequired)
                            {
                                hMessage.Control.Invoke(new MethodInvoker(() =>
                                {
                                    hMessage.DeleteMessageEffect();
                                }));
                            }
                            else
                            {
                                hMessage.DeleteMessageEffect();
                            }
                        }
                        break;
                    case PacketTypeEnum.NUMBEROFCHATLOAD:
                        Context.numberOfLoadChat = BitConverter.ToInt32(packet.Data, 0);
                        Context.count = 0;
                        break;
                    case PacketTypeEnum.DELETEFILE:
                        int index_file = Context.chatObjects.IndexOfKey(packet.createAt);
                        if(packet.From == Context.DestinationId && packet.To == Context.MyId && index_file != -1 && Context.chatObjects.Values[index_file] is MyFileInfo)
                        {
                            MyFileInfo fileInfo = (MyFileInfo)Context.chatObjects.Values[index_file];
                            if (fileInfo.Control.InvokeRequired)
                            {
                                fileInfo.Control.Invoke(new MethodInvoker(()=>fileInfo.DeleteFileEffect(IsImageFile(fileInfo.fileName))));
                            }
                            else
                            {
                                fileInfo.DeleteFileEffect(IsImageFile(fileInfo.fileName));
                            }
                        }
                        break;
                    case PacketTypeEnum.GROUPINFO:
                        break;
                    case PacketTypeEnum.ADDTOAGROUP:
                        break;
                    default:
                        break;
                }
            } while (packet != null);
        }
        public void SendFile(string pathFile,MainForm MainForm)
        {
            byte[] fileData = File.ReadAllBytes(pathFile);
            MyFileInfo file = new MyFileInfo(Path.GetFileName(pathFile), fileData,Context.MyId, DateTime.Now);
            PacketFile packetFile = new PacketFile(PacketTypeEnum.SENDFILE,fileData,Context.MyId,Context.DestinationId,file.CreateAt,Path.GetFileName(pathFile));
            Context.chatObjects.Add(file.CreateAt, file);
            MainForm.ShowChatObject(file,false);
            NetworkUtils.Write(Context.Writer, packetFile);
        }
        public void SendMessage(string text,MainForm MainForm)
        {
            SendMessage sendMessage = new SendMessage();
            sendMessage.Contents = text;
            Packet packet = new Packet(PacketTypeEnum.SENDMESSAGE, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendMessage)), Context.MyId, Context.DestinationId);
            HistoryMessage historyMessage = new HistoryMessage(Context.MyId, packet.createAt, text);
            Context.chatObjects.Add(packet.createAt,historyMessage);
            MainForm.ShowChatObject(historyMessage,false);
            NetworkUtils.Write(Context.Writer, packet);
        }
        public void LoadConservation(MainForm MainForm)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                if (Context.numberOfLoadChat != 0)
                {
                    for (int i = Context.numberOfLoadChat - 1; i >= 0; i--)
                        MainForm.ShowChatObject(Context.chatObjects.Values[i], true);
                    Context.lastChatLoad = Context.chatObjects.Values[0].CreateAt;
                }
                
                IsLoading = false;
            }
        }
        public static bool IsImageFile(string fileName)
        {
            if (fileName.Contains(".jpg") || fileName.Contains(".jpeg") || fileName.Contains(".bmp") || fileName.Contains(".png"))
            {
                return true;
            }
            return false;
        }
        public static ImageFormat GetImageFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".bmp":
                    return ImageFormat.Bmp;
                default:
                    return null;
            }
        }
    }
}
