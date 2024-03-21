using System;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using static LssClipBoard.LssPacket;
using System.Runtime.CompilerServices;

namespace LssClipBoard
{
    public partial class LssMain : Form
    {
        //ini파일쓰기
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        //ini파일 읽기
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder reVal, int size, string filepath);
        //핫키 등록
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        //핫키 해제
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        const int HOTKEY_ID = 4508; //내맘대로 키

        string LssIniFilePath = Application.StartupPath + @"\LssConfig.ini"; //ini 파일 경로

        string GlobalSendMsg; //전송메세지
        int MainPort = 0; //메인포트
        string MyIPAddr = ""; //내아이피
        int ByteSize = 4096; //기본 버퍼 사이즈

        Boolean LssFlag = true; //실행여부판단

        public LssMain()
        {
            InitializeComponent();
            LssInit();
            
            if (LssFlag)
            {
                MainPort = Convert.ToInt32(this.메인포트.Text);
                MyIPAddr = GetIPAddress();
                LssWork();
            }
        }

        public void LssInit()
        {
            //윈도우 시작이 최소화 상태로 시작
            this.WindowState = FormWindowState.Minimized;
            //작업표시줄에 표시 안함.
            //this.ShowInTaskbar = false;
            //윈폼 안보여줌
            //this.Visible = false;
            //트레이아이콘 보여줌
            this.notifyIcon1.Visible = true;
                       
            //ini 파일 읽을 버퍼 사이즈
            int BufferSize = 100;

            //저장값 불러오기
            StringBuilder sb;
            //전체컨트롤 가져오기
            List<Control> allControls = GetAllControls(this);

            foreach (Control control in allControls)
            {
                if (control.GetType().Name.Equals("TextBox"))
                {
                    sb = new StringBuilder();
                    GetPrivateProfileString("LssClipBoard", control.Name , "", sb, BufferSize, LssIniFilePath);
                    control.Text = sb.ToString();
                    if (control.Text.Trim().Length == 0)
                    {
                        LssFlag = false;
                        break;
                    }
                }
            }
        }

        public string GetIPAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            string empty = string.Empty;
            for (int i = 0; i < (int)hostEntry.AddressList.Length; i++)
            {
                if (hostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    empty = hostEntry.AddressList[i].ToString();
                    if (!empty.StartsWith("169"))
                    {
                        break;
                    }
                }
            }
            return empty;
        }
        /// <summary>
        /// 모든 컨트롤 찾기
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public List<Control> GetAllControls(Control control)
        {
            List<Control> allControls = new List<Control>();

            // 현재 컨트롤 추가
            allControls.Add(control);

            // 재귀적으로 모든 자식 컨트롤 추가
            foreach (Control childControl in control.Controls)
            {
                allControls.AddRange(GetAllControls(childControl));
            }

            return allControls;
        }
        /// <summary>
        /// 서버 시작
        /// </summary>
        private void LssWork()
        {
            Task task = Task.Run(() =>
            {
                LssServer();
            });
        }

        public void LssServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, MainPort);
            listener.Start();

            ConsoleLog(MyIPAddr + "[서버]서버시작");
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ConsoleLog("[서버]클라이언트접속");
                    // 클라이언트 데이터 수신
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[ByteSize];
                    int bytesRead;
                    StringBuilder data = new StringBuilder();

                    //this.Invoke(new Action(delegate ()
                    //{
                    //    Form lp = new Form()
                    //    {
                    //        FormBorderStyle = FormBorderStyle.None,
                    //        MinimizeBox = false,
                    //        MaximizeBox = false,
                    //        StartPosition = FormStartPosition.CenterScreen,
                    //        Name = "LssFileLoading",
                    //        Text = "데이터수신중",
                    //        Size = new System.Drawing.Size(200, 50)
                    //    };
                    //    ProgressBar progressbar = new ProgressBar()
                    //    {
                    //        Minimum = 0,
                    //        Maximum = 100,
                    //        Style = ProgressBarStyle.Marquee,
                    //        Dock = DockStyle.Fill
                    //    };
                    //    lp.Controls.Add(progressbar);
                    //    lp.Show();
                    //}));

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0){
                        data.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    }
                    //string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //ConsoleLog("수신데이터 : " + data);
                    LssPacket lssPacket = JsonConvert.DeserializeObject<LssPacket>(data.ToString());

                    ConsoleLog("[서버]수신데이터타입 : " + lssPacket.MessageType.ToString());
                    if (lssPacket.MessageType == LssMessageType.File)
                    {
                        ConsoleLog("[서버]수신파일카운트 : " + lssPacket.MessageFilesData.Count);
                    }

                    //다음 목적지가 있는지 찾아서 전달해줌.
                    if (lssPacket.MessageAddList.Length > 0 && lssPacket.MessageAddList.Contains("/"))
                    {
                        string NextAddrList = lssPacket.MessageAddList.Substring(lssPacket.MessageAddList.IndexOf("/")+1);
                        string NextAddr = "";
                        if (NextAddrList.Contains("/")){
                            NextAddr = NextAddrList.Substring(0, NextAddrList.IndexOf("/"));
                        }
                        else
                        {
                            NextAddr = NextAddrList;
                        }
                        
                        ConsoleLog("[서버]지금내위치 : " + NextAddr + " >> 배달주소 : " + NextAddrList);
                        lssPacket.MessageAddList  = NextAddrList;
                        lssPacket.MessageAddress  = NextAddr;
                        lssPacket.MessageData     = lssPacket.MessageData;
                        lssPacket.MessageType     = lssPacket.MessageType;
                        lssPacket.MessageFilesData = lssPacket.MessageFilesData;
                        LssSend(lssPacket);
                    }
                    else
                    {
                        //최종목적지면 클립보드 작업
                        LssInvokeClipboard(lssPacket);
                    }
                    ConsoleLog("[서버]수신완료");
                    client.Close();

                    //로딩창 닫기
                    //this.Invoke(new Action(delegate ()
                    //{
                    //    FormCollection fc = Application.OpenForms;

                    //    foreach (Form frm in fc)
                    //    {
                    //        //실행중인폼
                    //        if (frm.Name.Equals("LssFileLoading"))
                    //        {
                    //            frm.Close();
                    //            frm.Dispose();
                    //            break;
                    //        }
                    //    }
                    //}));
                }
                catch (Exception ex)
                {
                    ConsoleLog(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 목적지 도착했으면 클립보드에 작업해줌.
        /// </summary>
        /// <param name="lssPacket"></param>
        public void LssInvokeClipboard(LssPacket lssPacket)
        {
            Exception threadEx = null;

            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if (lssPacket.MessageType == LssMessageType.Text) //텍스트
                        {
                            Clipboard.SetText(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(lssPacket.MessageData)));
                        }
                        else if (lssPacket.MessageType == LssMessageType.Image) //이미지
                        {
                            // Base64 문자열을 바이트 배열로 디코딩
                            byte[] bytes = Convert.FromBase64String(lssPacket.MessageData);

                            // 디코딩된 바이트 배열로부터 이미지 생성
                            using (MemoryStream memoryStream = new MemoryStream(bytes))
                            {
                                Image image = Image.FromStream(memoryStream);
                                Clipboard.SetImage(image);
                            }
                        }
                        else if (lssPacket.MessageType == LssMessageType.File) //파일
                        {
                            //바탕화면에 컴퓨터 이름이 기본값
                            string SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + SystemInformation.ComputerName;

                            DirectoryInfo di = new DirectoryInfo(SaveDirectory);

                            if (di.Exists == false)
                            {
                                di.Create();
                            }
                            
                            StringCollection filePaths = new StringCollection();
                            //progressBar.Step =  Convert.ToInt32(Math.Ceiling(Convert.ToDouble(100/lssPacket.MessageFilesData.Count)));

                            foreach (var FileData in lssPacket.MessageFilesData)
                            {
                                // Base64 문자열을 바이트 배열로 디코딩
                                byte[] bytes = Convert.FromBase64String(FileData.FileContents);

                                string SaveFileName = SaveDirectory + "\\" + FileData.FileName;

                                //File.WriteAllBytes(SaveFileName, bytes);

                                using (var stream = new FileStream(SaveFileName, FileMode.Create, FileAccess.Write))
                                {
                                    stream.Write(bytes, 0, bytes.Length);
                                }

                                //File.SetAttributes(SaveFileName, File.GetAttributes(SaveFileName) | FileAttributes.Hidden);

                                filePaths.Add(SaveFileName);
                            }
                            //progressBar.Value = progressBar.Maximum;

                            //Clipboard.SetFileDropList(filePaths);
                            byte[] MoveEffect = new byte[] { 2, 0, 0, 0 }; //무브효과
                            MemoryStream DropEffect = new MemoryStream();
                            DropEffect.Write(MoveEffect, 0, MoveEffect.Length);

                            DataObject data = new DataObject();
                            data.SetFileDropList(filePaths);
                            data.SetData("Preferred DropEffect", DropEffect);

                            Clipboard.Clear();
                            Clipboard.SetDataObject(data, true);
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            
        }
        /// <summary>
        /// 전송메소드
        /// </summary>
        /// <param name="packet"></param>
        public void LssSend(LssPacket packet)
        {
            if (packet != null)
            {
                if (packet.MessageAddress.Length > 0)
                {
                    ConsoleLog("[송신]전송준비 : " + packet.MessageAddress);
                    TcpClient client = new TcpClient(packet.MessageAddress, MainPort);
                    ConsoleLog("[송신]전송시작");
                    NetworkStream stream;
                    //ConsoleLog("[송신]전송준비2");
                    GlobalSendMsg = JsonConvert.SerializeObject(packet);
                    //ConsoleLog("[송신]전송준비3");
                    byte[] data = Encoding.UTF8.GetBytes(GlobalSendMsg);
                    try
                    {
                        stream = client.GetStream();
                        // 데이터 송신
                        stream.Write(data, 0, data.Length);
                        ConsoleLog("[송신]데이터 전송완료");
                    }
                    catch (Exception ex)
                    {
                        ConsoleLog("[송신]전송 중 오류 발생: " + ex.Message);
                        MessageBox.Show("[송신]전송 중 오류 발생: " + ex.Message);
                    }
                    finally
                    {
                        client.Close();
                    }
                }
            }
        }

        public void LssSendInit(string start , string end)
        {
            LssPacket packet = new LssPacket();
            
            IDataObject dataObject = Clipboard.GetDataObject();

            if (dataObject != null)
            {
                //배달지를 dic 으로 만들어줌.
                Dictionary<string, int> LssComList = new Dictionary<string, int>();

                LssComList.Add(this.업무_VDI_IP.Text, 0);

                LssComList.Add(this.업무_PC_IP.Text, 1);
                LssComList.Add(this.업무_FF_IP.Text, 1);

                LssComList.Add(this.개발_PC_IP.Text, 2);
                LssComList.Add(this.개발_FF_IP.Text, 2);

                LssComList.Add(this.개발_VDI_IP.Text, 3);

                packet.MessageAddress = "";
                packet.MessageAddList = "";

                //배달 주소 셋팅
                if (end.Equals("반대쪽"))
                {
                    if (MyIPAddr.Equals(this.업무_VDI_IP.Text))
                    {
                        packet.MessageAddress = this.업무_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_PC_IP.Text + "/" + this.개발_FF_IP.Text + "/" + this.개발_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }

                    if (MyIPAddr.Equals(this.개발_VDI_IP.Text))
                    {
                        packet.MessageAddress = this.개발_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_PC_IP.Text + "/" + this.업무_FF_IP.Text + "/" + this.업무_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                }
                else if (LssComList[start] == 0)
                {
                    if (LssComList[end] == 1)
                    {
                        packet.MessageAddress = this.업무_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_PC_IP.Text; //배달가야할 주소
                    }
                    else if (LssComList[end] == 2)
                    {
                        packet.MessageAddress = this.업무_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_PC_IP.Text + "/" + this.개발_FF_IP.Text; //배달가야할 주소
                    }
                    else if (LssComList[end] == 3)
                    {
                        packet.MessageAddress = this.업무_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_PC_IP.Text + "/" + this.개발_FF_IP.Text + "/" + this.개발_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                }
                else if (LssComList[start] == 1)
                {
                    if (LssComList[end] == 0)
                    {
                        packet.MessageAddress = this.업무_VDI_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_VDI_IP.Text; //배달가야할 주소
                    }
                    else if (LssComList[end] == 2)
                    {
                        packet.MessageAddress = this.개발_FF_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_FF_IP.Text; //배달가야할 주소
                    }
                    else if (LssComList[end] == 3)
                    {
                        packet.MessageAddress = this.개발_FF_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_FF_IP.Text + "/" + this.개발_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                }
                else if (LssComList[start] == 2)
                {
                    if (LssComList[end] == 0)
                    {
                        packet.MessageAddress = this.업무_FF_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_FF_IP.Text + "/" + this.업무_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                    else if (LssComList[end] == 1)
                    {
                        packet.MessageAddress = this.업무_FF_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.업무_FF_IP.Text; //배달가야할 주소
                    }
                    else if (LssComList[end] == 3)
                    {
                        packet.MessageAddress = this.개발_VDI_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                }
                else if (LssComList[start] == 3)
                {
                    if (LssComList[end] == 0)
                    {
                        packet.MessageAddress = this.개발_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_PC_IP.Text + "/" + this.업무_FF_IP.Text + "/" + this.업무_VDI_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                    else if (LssComList[end] == 1)
                    {
                        packet.MessageAddress = this.개발_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_PC_IP.Text + "/" + this.업무_FF_IP.Text; //전체배달주소(단계별로 가면서 잘라냄)
                    }
                    else if (LssComList[end] == 2)
                    {
                        packet.MessageAddress = this.개발_PC_IP.Text; //배달가야할 주소
                        packet.MessageAddList = this.개발_PC_IP.Text; //배달가야할 주소
                    }

                }
                if (SystemInformation.ComputerName.Equals("DESKTOP-7VPMS65"))
                {
                    packet.MessageAddress = "192.168.0.3";
                    packet.MessageAddList = "192.168.0.3";
                }

                if (dataObject.GetDataPresent(DataFormats.FileDrop)) //클립보드에 파일이 있는지 확인
                {
                    packet.MessageType = LssMessageType.File;
                    string[] FileList = (string[])dataObject.GetData(DataFormats.FileDrop);
                    
                    packet.MessageFilesData = new List<MessageFiles>(); //초기화

                    foreach (var FileInfo in FileList)
                    {
                        //using (MemoryStream memoryStream = new MemoryStream())
                        //{
                        //    MessageFiles files = new MessageFiles();
                        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
                        //    byte[] numArray = File.ReadAllBytes(FileInfo);
                        //    files.FileContents = Convert.ToBase64String(numArray);
                        //    FileInfo file = new FileInfo(FileInfo);
                        //    files.FileName = file.Name;
                        //    packet.MessageFilesData.Add(files);
                        //}
                        FileInfo file = new FileInfo(FileInfo);
                        //82657192
                        if (file.Length > 83000000)
                        {
                            MessageBox.Show("용량이 너무 큽니다.");
                        }
                        else
                        {
                            var binary = new byte[file.Length];
                            using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                            {
                                MessageFiles files = new MessageFiles();
                                stream.Read(binary, 0, binary.Length);
                                files.FileName = file.Name;
                                files.FileContents = Convert.ToBase64String(binary);
                                packet.MessageFilesData.Add(files);
                            }
                            LssSend(packet);
                        }
                    }
                } 
                else if (Clipboard.ContainsText()) //클립보드에 텍스트가 있는지 확인
                {
                    packet.MessageType = LssMessageType.Text;
                    packet.MessageData = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Clipboard.GetText()));
                    LssSend(packet);
                }
                else if (Clipboard.ContainsImage()) //클립보드에 이미지가 있는지 확인
                {
                    packet.MessageType = LssMessageType.Image;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        Image image = Clipboard.GetImage();

                        image.Save(memoryStream, ImageFormat.Jpeg);
                        byte[] imageBytes = memoryStream.ToArray();
                        packet.MessageData = Convert.ToBase64String(imageBytes);
                        LssSend(packet);
                    }
                }
            }
        }

        public ImageCodecInfo GetEncoder(ImageFormat format) //이미지 인코딩 인포
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void ConsoleLog(string msg) //로그
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    this.LssLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    this.LssLog.AppendText(" >> ");
                    this.LssLog.AppendText(msg);
                    this.LssLog.AppendText(Environment.NewLine);
                    this.LssLog.ScrollToCaret();
                }
                ));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LssSave()
        {
            WritePrivateProfileString("LssClipBoard", "메인포트"    , this.메인포트.Text.Trim(), LssIniFilePath);
            WritePrivateProfileString("LssClipBoard", "업무_VDI_IP", this.업무_VDI_IP.Text.Trim(), LssIniFilePath);
            WritePrivateProfileString("LssClipBoard", "개발_VDI_IP", this.개발_VDI_IP.Text.Trim(), LssIniFilePath);
            WritePrivateProfileString("LssClipBoard", "업무_PC_IP" , this.업무_PC_IP.Text.Trim(), LssIniFilePath);
            WritePrivateProfileString("LssClipBoard", "업무_FF_IP" , this.업무_FF_IP.Text.Trim(), LssIniFilePath);
            WritePrivateProfileString("LssClipBoard", "개발_PC_IP" , this.개발_PC_IP.Text.Trim(), LssIniFilePath);
            WritePrivateProfileString("LssClipBoard", "개발_FF_IP" , this.개발_FF_IP.Text.Trim(), LssIniFilePath);
        }
        private void 레지스터등록()
        {
            if (MyIPAddr.Equals(업무_VDI_IP.Text) || MyIPAddr.Equals(개발_VDI_IP.Text))
            {
                RegisterHotKey(base.Handle, 94508, (uint)KeyModifiers.Control, (uint)Keys.D1);
            }            
        }

        private void 레지스터해제()
        {
            if (MyIPAddr.Equals(업무_VDI_IP.Text) || MyIPAddr.Equals(개발_VDI_IP.Text))
            {
                UnregisterHotKey(base.Handle, 1);
                UnregisterHotKey(base.Handle, 91484508);
                UnregisterHotKey(base.Handle, 94508);
            }
        }

        //키보드 이벤트
        const int WM_HOTKEY = 0x0312;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == 94508)
            {
                ButtonClick();
            }            
        }

        private void ButtonClick()
        {
            LssSendInit(MyIPAddr, "반대쪽");
        }

        private void BTN_시작_Click(object sender, EventArgs e)
        {
            LssSave();
            LssWork();
        }

        private void BTN_전송_Click(object sender, EventArgs e)
        {
            ButtonClick();
        }

        private void BTN_클리어_Click(object sender, EventArgs e)
        {
            LssLog.Clear();
        }

        private void 종료_toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LssMain_Load(object sender, EventArgs e)
        {
            레지스터해제();
            레지스터등록();
        }

        private void LssMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                레지스터해제();
            }
        }

        private void LssMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                this.Hide(); // 폼 숨기기
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show(); // 폼 보이기
            this.WindowState = FormWindowState.Normal; // 최소화 해제
        }

        private void BTN_파일_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false
            };

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StringCollection filePaths = new StringCollection();

                filePaths.Add(openFileDialog.FileName);
                
                Clipboard.SetFileDropList(filePaths);
            }
        }

        private void SEND_업무VDI_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false
            };

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StringCollection filePaths = new StringCollection();

                filePaths.Add(openFileDialog.FileName);

                Clipboard.SetFileDropList(filePaths);
            }

            LssSendInit(MyIPAddr, this.업무_VDI_IP.Text);
        }

        private void SEND_개발VDI_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false
            };

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StringCollection filePaths = new StringCollection();

                filePaths.Add(openFileDialog.FileName);

                Clipboard.SetFileDropList(filePaths);
            }

            LssSendInit(MyIPAddr, this.개발_VDI_IP.Text);
        }

        private void SEND_업무PC_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false
            };

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StringCollection filePaths = new StringCollection();

                filePaths.Add(openFileDialog.FileName);

                Clipboard.SetFileDropList(filePaths);
            }

            LssSendInit(MyIPAddr, this.업무_PC_IP.Text);
        }

        private void SEND_개발PC_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true
            };

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StringCollection filePaths = new StringCollection();

                filePaths.Add(openFileDialog.FileName);

                Clipboard.SetFileDropList(filePaths);
            }

            LssSendInit(MyIPAddr, this.개발_PC_IP.Text);
        }
    }

    //전송데이터 패킷
    public class LssPacket
    {
        public string MessageAddress { get; set; } //배달주소
        public string MessageAddList { get; set; } //배달주소리스트
        public LssMessageType MessageType { get; set; } //메시지타입
        public string MessageData { get; set; } //메세지내용
        public List<MessageFiles> MessageFilesData { get; set; } //파일데이터(여러개 가능)

        public class MessageFiles
        {
            public string FileName { get; set; }
            public string FileContents { get; set; }
        }
    }

    //메시지타입
    public enum LssMessageType
    {
        Text,Image,File
    }
}