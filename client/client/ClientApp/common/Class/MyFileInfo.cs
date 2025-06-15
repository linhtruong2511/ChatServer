using ClientApp.Properties;
using ClientApp.utils;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ClientApp.common.Class
{
    public class MyFileInfo : ChatObject
    {
        public static int fileId { get; set; } = 0;
        public string fileName { get; set; }
        public byte[] fileData { get; set; }

        public MyFileInfo(string fileName, byte[] fileData, int source, DateTime createAt)
        {
            fileId++;
            this.fileName = fileName;
            this.fileData = fileData;
            this.Source = source;
            this.CreateAt = createAt;
            if (Controller.IsImageFile(fileName))
            {
                using (MemoryStream ms = new MemoryStream(fileData))
                {
                    using (Image temp = Image.FromStream(ms))
                    {
                        Image image = new Bitmap(temp);
                        this.Control = new PictureBox
                        {
                            Image = image,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Padding = new Padding(10),
                        };
                    }
                    ((PictureBox)Control).MouseClick += (sender, ea1) =>                                                                                                                  
                    {
                        if (ea1.Button == MouseButtons.Right&&!IsDeleted)
                        {
                            ContextMenuStrip ctms = new ContextMenuStrip();
                            if (this.Source == Context.MyId)
                            {
                                ctms.Items.Add("xoá ảnh", null, (s, ea2) =>
                                {
                                    if (!IsDeleted && this.Source == Context.MyId)
                                    {
                                        IsDeleted = true;
                                        NetworkUtils.Write(Context.Writer, new Packet(Enum.PacketTypeEnum.DELETEFILE, this.fileData, Context.MyId, Context.DestinationId, this.CreateAt));
                                        ((PictureBox)this.Control).Image = Resources.xoaanh;
                                    }
                                });
                            }
                            ctms.Items.Add("lưu ảnh", null, (s, ea2) =>
                            {
                                SaveFileDialog sfd_ImageSaved = new SaveFileDialog
                                {
                                    Filter = "ảnh png|*.png|ảnh jpg|*.jpg|ảnh jpeg|*.jpeg|ảnh bmp|*.bmp",
                                    Title = "Lưu ảnh",
                                };
                                if (sfd_ImageSaved.ShowDialog() == DialogResult.OK)
                                {
                                    ImageFormat format = Controller.GetImageFormat(Path.GetExtension(sfd_ImageSaved.FileName));
                                    ((PictureBox)Control).Image.Save(sfd_ImageSaved.FileName, format);
                                }
                            });
                            ctms.Show(Cursor.Position);
                        }
                        else if(ea1.Button == MouseButtons.Left && !IsDeleted)
                        {
                            Task.Run(() =>
                            {
                                ((PictureBox)this.Control).Invoke(new MethodInvoker(() =>
                                {
                                    Form showpicture = new Form
                                    {
                                        Size = new Size(800, 600),
                                    };
                                    PictureBox pb = new PictureBox
                                    {
                                        Image = ((PictureBox)this.Control).Image,
                                        SizeMode = PictureBoxSizeMode.Zoom,
                                        Dock = DockStyle.Fill,
                                    };
                                    showpicture.Controls.Add(pb);
                                    showpicture.Show();
                                }));
                            });
                        }
                    };
                }
            }
            else
            {
                this.Control = new Label
                {
                    Text = fileName,
                    AutoSize = true,
                    ForeColor = Color.Blue,
                    Font = new Font("Arial", 10, FontStyle.Italic),
                };
                ((Label)Control).MouseClick += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right&&this.Source == Context.MyId&&!IsDeleted)
                    {
                        ContextMenuStrip ctms = new ContextMenuStrip();
                        ctms.Items.Add("xoá file", null, (s, es) =>
                        {
                            if (!IsDeleted && this.Source == Context.MyId)
                            {
                                IsDeleted = true;
                                NetworkUtils.Write(Context.Writer, new Packet(Enum.PacketTypeEnum.DELETEFILE, this.fileData, Context.MyId, Context.DestinationId, this.CreateAt));
                                ((Label)this.Control).Text = "file đã xoá";
                                ((Label)this.Control).Font = new Font("Arial", 10, FontStyle.Italic);
                                ((Label)this.Control).ForeColor = Color.Red;
                            }
                        });
                        ctms.Show(Cursor.Position);
                    }
                    if(e.Button == MouseButtons.Left&&!IsDeleted)
                    {
                        SaveFileDialog sfd_FileSaved = new SaveFileDialog
                        {
                            Filter = "WORD|*.docx|PDF|*.pdf|EXCEL|*.xlss|TEXT|*.txt",
                            Title = "Lưu File"
                        };
                        if (sfd_FileSaved.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllBytes(sfd_FileSaved.FileName, this.fileData);
                        }
                    }
                };
            }
        }
        public void DeleteFileEffect(bool IsImage)
        {
            this.IsDeleted = true;
            if (IsImage)
            {
                ((PictureBox)this.Control).Image = Resources.xoaanh;
            }
            else
            {
                ((Label)this.Control).Text = "file đã xoá";
                ((Label)this.Control).Font = new Font("Arial", 10, FontStyle.Italic);
                ((Label)this.Control).ForeColor = Color.Red;
            }
        }
    }
}
