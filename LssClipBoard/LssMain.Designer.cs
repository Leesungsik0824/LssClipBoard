namespace LssClipBoard
{
    partial class LssMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LssMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.업무_VDI_IP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.개발_VDI_IP = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.업무_FF_IP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.업무_PC_IP = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.개발_FF_IP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.개발_PC_IP = new System.Windows.Forms.TextBox();
            this.BTN_시작 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.메인포트 = new System.Windows.Forms.TextBox();
            this.LssLog = new System.Windows.Forms.RichTextBox();
            this.BTN_전송 = new System.Windows.Forms.Button();
            this.BTN_클리어 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SEND_업무VDI = new System.Windows.Forms.ToolStripMenuItem();
            this.SEND_개발VDI = new System.Windows.Forms.ToolStripMenuItem();
            this.SEND_업무PC = new System.Windows.Forms.ToolStripMenuItem();
            this.SEND_개발PC = new System.Windows.Forms.ToolStripMenuItem();
            this.종료_toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.BTN_파일 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.업무_VDI_IP);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "업무VDI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "아이피";
            // 
            // 업무_VDI_IP
            // 
            this.업무_VDI_IP.Location = new System.Drawing.Point(50, 19);
            this.업무_VDI_IP.Name = "업무_VDI_IP";
            this.업무_VDI_IP.Size = new System.Drawing.Size(144, 21);
            this.업무_VDI_IP.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.개발_VDI_IP);
            this.groupBox2.Location = new System.Drawing.Point(219, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 49);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "개발VDI";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "아이피";
            // 
            // 개발_VDI_IP
            // 
            this.개발_VDI_IP.Location = new System.Drawing.Point(50, 19);
            this.개발_VDI_IP.Name = "개발_VDI_IP";
            this.개발_VDI_IP.Size = new System.Drawing.Size(144, 21);
            this.개발_VDI_IP.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.업무_FF_IP);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.업무_PC_IP);
            this.groupBox3.Location = new System.Drawing.Point(12, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(201, 75);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "업무PC";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "사설";
            // 
            // 업무_FF_IP
            // 
            this.업무_FF_IP.Location = new System.Drawing.Point(50, 46);
            this.업무_FF_IP.Name = "업무_FF_IP";
            this.업무_FF_IP.Size = new System.Drawing.Size(144, 21);
            this.업무_FF_IP.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "아이피";
            // 
            // 업무_PC_IP
            // 
            this.업무_PC_IP.Location = new System.Drawing.Point(50, 19);
            this.업무_PC_IP.Name = "업무_PC_IP";
            this.업무_PC_IP.Size = new System.Drawing.Size(144, 21);
            this.업무_PC_IP.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.개발_FF_IP);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.개발_PC_IP);
            this.groupBox4.Location = new System.Drawing.Point(219, 93);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(201, 75);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "개발PC";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "사설";
            // 
            // 개발_FF_IP
            // 
            this.개발_FF_IP.Location = new System.Drawing.Point(50, 46);
            this.개발_FF_IP.Name = "개발_FF_IP";
            this.개발_FF_IP.Size = new System.Drawing.Size(144, 21);
            this.개발_FF_IP.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "아이피";
            // 
            // 개발_PC_IP
            // 
            this.개발_PC_IP.Location = new System.Drawing.Point(50, 19);
            this.개발_PC_IP.Name = "개발_PC_IP";
            this.개발_PC_IP.Size = new System.Drawing.Size(144, 21);
            this.개발_PC_IP.TabIndex = 0;
            // 
            // BTN_시작
            // 
            this.BTN_시작.Location = new System.Drawing.Point(12, 195);
            this.BTN_시작.Name = "BTN_시작";
            this.BTN_시작.Size = new System.Drawing.Size(165, 31);
            this.BTN_시작.TabIndex = 6;
            this.BTN_시작.Text = "서버시작";
            this.BTN_시작.UseVisualStyleBackColor = true;
            this.BTN_시작.Click += new System.EventHandler(this.BTN_시작_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "메인포트";
            // 
            // 메인포트
            // 
            this.메인포트.Location = new System.Drawing.Point(77, 6);
            this.메인포트.Name = "메인포트";
            this.메인포트.Size = new System.Drawing.Size(100, 21);
            this.메인포트.TabIndex = 8;
            // 
            // LssLog
            // 
            this.LssLog.Location = new System.Drawing.Point(12, 229);
            this.LssLog.Name = "LssLog";
            this.LssLog.Size = new System.Drawing.Size(410, 316);
            this.LssLog.TabIndex = 9;
            this.LssLog.Text = "";
            // 
            // BTN_전송
            // 
            this.BTN_전송.Location = new System.Drawing.Point(183, 195);
            this.BTN_전송.Name = "BTN_전송";
            this.BTN_전송.Size = new System.Drawing.Size(80, 31);
            this.BTN_전송.TabIndex = 10;
            this.BTN_전송.Text = "전송";
            this.BTN_전송.UseVisualStyleBackColor = true;
            this.BTN_전송.Click += new System.EventHandler(this.BTN_전송_Click);
            // 
            // BTN_클리어
            // 
            this.BTN_클리어.Location = new System.Drawing.Point(347, 195);
            this.BTN_클리어.Name = "BTN_클리어";
            this.BTN_클리어.Size = new System.Drawing.Size(73, 31);
            this.BTN_클리어.TabIndex = 11;
            this.BTN_클리어.Text = "로그클리어";
            this.BTN_클리어.UseVisualStyleBackColor = true;
            this.BTN_클리어.Click += new System.EventHandler(this.BTN_클리어_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "LssClipBoard";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SEND_업무VDI,
            this.SEND_개발VDI,
            this.SEND_업무PC,
            this.SEND_개발PC,
            this.종료_toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 114);
            // 
            // SEND_업무VDI
            // 
            this.SEND_업무VDI.Name = "SEND_업무VDI";
            this.SEND_업무VDI.Size = new System.Drawing.Size(182, 22);
            this.SEND_업무VDI.Text = "업무VDI로 파일전송";
            this.SEND_업무VDI.Click += new System.EventHandler(this.SEND_업무VDI_Click);
            // 
            // SEND_개발VDI
            // 
            this.SEND_개발VDI.Name = "SEND_개발VDI";
            this.SEND_개발VDI.Size = new System.Drawing.Size(182, 22);
            this.SEND_개발VDI.Text = "개발VDI로 파일전송";
            this.SEND_개발VDI.Click += new System.EventHandler(this.SEND_개발VDI_Click);
            // 
            // SEND_업무PC
            // 
            this.SEND_업무PC.Name = "SEND_업무PC";
            this.SEND_업무PC.Size = new System.Drawing.Size(182, 22);
            this.SEND_업무PC.Text = "업무PC로 파일전송";
            this.SEND_업무PC.Click += new System.EventHandler(this.SEND_업무PC_Click);
            // 
            // SEND_개발PC
            // 
            this.SEND_개발PC.Name = "SEND_개발PC";
            this.SEND_개발PC.Size = new System.Drawing.Size(182, 22);
            this.SEND_개발PC.Text = "개발PC로 파일전송";
            this.SEND_개발PC.Click += new System.EventHandler(this.SEND_개발PC_Click);
            // 
            // 종료_toolStripMenuItem1
            // 
            this.종료_toolStripMenuItem1.Name = "종료_toolStripMenuItem1";
            this.종료_toolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.종료_toolStripMenuItem1.Text = "종료";
            this.종료_toolStripMenuItem1.Click += new System.EventHandler(this.종료_toolStripMenuItem1_Click);
            // 
            // BTN_파일
            // 
            this.BTN_파일.Location = new System.Drawing.Point(269, 195);
            this.BTN_파일.Name = "BTN_파일";
            this.BTN_파일.Size = new System.Drawing.Size(72, 31);
            this.BTN_파일.TabIndex = 12;
            this.BTN_파일.Text = "파일";
            this.BTN_파일.UseVisualStyleBackColor = true;
            this.BTN_파일.Click += new System.EventHandler(this.BTN_파일_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(16, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(336, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "※ 파일을 선택하였을 경우 , 파일전송이 우선시 됩니다.";
            // 
            // LssMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 557);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BTN_파일);
            this.Controls.Add(this.BTN_클리어);
            this.Controls.Add(this.BTN_전송);
            this.Controls.Add(this.LssLog);
            this.Controls.Add(this.메인포트);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTN_시작);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LssMain";
            this.Text = "LssClipBoard㈜";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LssMain_FormClosing);
            this.Load += new System.EventHandler(this.LssMain_Load);
            this.Resize += new System.EventHandler(this.LssMain_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox 업무_VDI_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox 개발_VDI_IP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox 업무_FF_IP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox 업무_PC_IP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox 개발_FF_IP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox 개발_PC_IP;
        private System.Windows.Forms.Button BTN_시작;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox 메인포트;
        private System.Windows.Forms.RichTextBox LssLog;
        private System.Windows.Forms.Button BTN_전송;
        private System.Windows.Forms.Button BTN_클리어;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 종료_toolStripMenuItem1;
        private System.Windows.Forms.Button BTN_파일;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem SEND_업무VDI;
        private System.Windows.Forms.ToolStripMenuItem SEND_개발VDI;
        private System.Windows.Forms.ToolStripMenuItem SEND_업무PC;
        private System.Windows.Forms.ToolStripMenuItem SEND_개발PC;
    }
}

