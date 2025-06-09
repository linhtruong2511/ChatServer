using System;
using System.Windows.Forms;
using System.Drawing;
using ClientApp.utils;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.common.Class
{
    public class HistoryMessage : ChatObject
    {
        public string Contents { get; set; }
        public HistoryMessage() { }
        public HistoryMessage(int source, DateTime createAt, string contents)
        {
            this.Source = source;
            this.CreateAt = createAt;
            this.Contents = contents;
            this.Control = new Label
            {
                Text = contents,
                AutoSize = true,
                Padding = new Padding(10)
            };
            ((Label)Control).MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right && this.Source == Context.MyId)
                {
                    ContextMenuStrip ctms = new ContextMenuStrip();

                    ctms.Items.Add("xoá tin nhắn", null, (s, ev) =>
                    {
                        if (!IsDeleted && this.Source == Context.MyId)
                        {
                            IsDeleted = true;
                            NetworkUtils.Write(Context.Writer, new Packet(Enum.PacketTypeEnum.DELETEMESSAGE, Encoding.UTF8.GetBytes(((Label)sender).Text), Context.MyId, Context.DestinationId, this.CreateAt));
                            DeleteMessageEffect();
                        }
                    });
                    ctms.Items.Add("sửa tin nhắn", null, (s, ev) =>
                    {
                        if (this.Source == Context.MyId)
                        {
                            Console.WriteLine("Is there");
                            NetworkUtils.Write(Context.Writer, new Packet(Enum.PacketTypeEnum.CHANGEMESSAGE, Encoding.UTF8.GetBytes(((Label)sender).Text), Context.MyId, Context.DestinationId, this.CreateAt));
                        }
                        //await Task.Run(() =>
                        //{
                        //    Form changeMessage = new Form
                        //    {
                        //        Size = new Size(200,100)
                        //    };
                        //    TextBox txt_changeMessage = new TextBox();
                        //    if
                        //})
                    });
                    ctms.Show(Cursor.Position);
                }
            };
        }
        public void DeleteMessageEffect()
        {
            ((Label)this.Control).Text = "tin nhắn đã xoá";
            ((Label)this.Control).Font = new Font("Arial", 10, FontStyle.Italic);
            ((Label)this.Control).ForeColor = Color.Red;
        }
    }
}
