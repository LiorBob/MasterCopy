using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;





namespace MasterCopy
{
    public partial class frmMasterCopy : Form
    {
        private int firstFreeIndex = 0;
        private bool areAllImagesSelected = false;
        private bool isInSelectAllOrCopyMode = false;

        


        public frmMasterCopy()
        {
            InitializeComponent();

            lstLatestClipboardContent.GotFocus += new EventHandler(lstLatestClipboardContent_GotFocus);
            lstLatestClipboardFiles.GotFocus += new EventHandler(lstLatestClipboardFiles_GotFocus);



            this.WindowState = FormWindowState.Maximized;
            DrawPictureBoxes();

            Clipboard.Clear();

        }





        private void DrawPictureBoxes()
        {
            int x = 355;
            int y = 55;


            int totalNumberOfImages = 5 * 5;


            for (int i = 1; i <= totalNumberOfImages; i++)
            {
                if ((i % 5 == 1) && (i / 5 > 0))
                {
                    x = 355;
                    y += 80;
                }



                PictureBox pictureBox = new PictureBox();

                pictureBox.Location = new Point(x, y);
                pictureBox.Size = new Size(70, 70);

                pictureBox.BackColor = System.Drawing.Color.White;
                pictureBox.BorderStyle = BorderStyle.Fixed3D;

                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;


                pictureBox.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                pictureBox.DoubleClick += new EventHandler(pictureBox_DoubleClick);


                this.Controls.Add(pictureBox);

                
                x += 80;

            }



            lstLatestClipboardContent.Height = y - lstLatestClipboardContent.Location.Y + 80;

        }




        private void tmrCheckNewClipboardContent_Tick(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                AddToTextListBox();
            }


            if (Clipboard.ContainsImage())
            {
                AddToPictureBox();
            }


            if (Clipboard.ContainsFileDropList())
            {
                AddToFileListBox();
            }

        }





        private void AddToTextListBox()
        {
            string latestClipboardContent = Clipboard.GetText();


            if (latestClipboardContent.Trim() != String.Empty)
            {
                string trimmedLatestClipboardContent = latestClipboardContent.Trim();


                if (!lstLatestClipboardContent.Items.Contains(latestClipboardContent))
                {
                    if (!lstLatestClipboardContent.Items.Contains(trimmedLatestClipboardContent))
                    {
                        if (trimmedLatestClipboardContent.Contains("\r\n"))
                        {
                            string[] allLines = trimmedLatestClipboardContent.Split(new string[] {"\r\n"}, StringSplitOptions.None);


                            foreach (string line in allLines)
                            {
                                if (! lstLatestClipboardContent.Items.Contains(line))
                                {
                                    lstLatestClipboardContent.Items.Add(line);
                                }
                            }

                            
                        }


                        else
                        {
                            lstLatestClipboardContent.Items.Add(trimmedLatestClipboardContent);
                        }
                    }

                }

            }

        }




        
        private void AddToPictureBox()
        {
            IEnumerable<PictureBox> pictureBoxes = this.Controls.OfType<PictureBox>();
            PictureBox[] picBoxes = pictureBoxes.ToArray();

            Bitmap latestClipboardContent = (Bitmap)Clipboard.GetImage();

            bool isBitmapAlreadyInBitmapsList = false;




            foreach (PictureBox pictureBox in pictureBoxes)
            {
                Bitmap bmp = (Bitmap)pictureBox.Image;



                // if bmp is null, we skip doImagesMatch, so we insert the new picture to the 1st blank picturebox

                if (bmp == null)
                {
                    break;
                }


                if (doImagesMatch(bmp, latestClipboardContent))
                {
                    isBitmapAlreadyInBitmapsList = true;
                    break;
                }

            }



            if (! isBitmapAlreadyInBitmapsList)
            {
                picBoxes[firstFreeIndex].Image = latestClipboardContent;
                firstFreeIndex = (firstFreeIndex + 1) % pictureBoxes.Count();
            }

        }





        private void AddToFileListBox()
        {
            StringCollection namesOfClipboardFiles = Clipboard.GetFileDropList();
            string[] filesNames = new string[namesOfClipboardFiles.Count];

            namesOfClipboardFiles.CopyTo(filesNames, 0);


            List<string> filesOnClipboard = new List<string>(filesNames);
            filesOnClipboard.Sort();




            string filesNamesOnClipboard = "";



            foreach (string fileOnClipboard in filesOnClipboard)
            {
                filesNamesOnClipboard += fileOnClipboard + ",";
            }



            filesNamesOnClipboard = filesNamesOnClipboard.Remove(filesNamesOnClipboard.Length - 1);
            


            if (!lstLatestClipboardFiles.Items.Contains(filesNamesOnClipboard))
            {
                lstLatestClipboardFiles.Items.Add(filesNamesOnClipboard);
            }

        }





        private void lstLatestClipboardContent_KeyDown(object sender, KeyEventArgs e)
        {
            isInSelectAllOrCopyMode = false;




            // Enables pressing Del key, to delete selected item

            if (e.KeyData == Keys.Delete)
            {
                int count = lstLatestClipboardContent.SelectedItems.Count;


                for (int i = 0; i < count; i++)
                {
                    if (lstLatestClipboardContent.SelectedItems[0] != null)
                    {
                        lstLatestClipboardContent.Items.Remove(lstLatestClipboardContent.SelectedItems[0]);
                        Clipboard.Clear();
                    }

                }

            }




            // Enables Ctrl+A (select all)

            if (e.KeyCode == Keys.A && e.Control)
            {
                isInSelectAllOrCopyMode = true;


                for (int i = 0; i < lstLatestClipboardContent.Items.Count; i++)
                {
                    lstLatestClipboardContent.SetSelected(i, true);
                }

            }





            // Enables Ctrl+C :  copies the selected items to clipboard

            if (e.KeyCode == Keys.C && e.Control)
            {
                isInSelectAllOrCopyMode = true;



                string selectedContent = "";


                for (int i = 0; i < lstLatestClipboardContent.SelectedItems.Count; i++)
                {
                    if (lstLatestClipboardContent.SelectedItems[i] != null)
                    {
                        string selectedItemContent = lstLatestClipboardContent.SelectedItems[i].ToString();


                        if (selectedItemContent != String.Empty)
                        {
                            selectedContent += selectedItemContent + "\r\n";
                        }

                    }
                    
                }


                Clipboard.SetText(selectedContent);

            }

        }





        private void lstLatestClipboardContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isInSelectAllOrCopyMode)                 
            {
                e.Handled = true;                   // Blocks keyboard input if we pressed Ctrl+A or Ctrl+C
            }
        }







        private void lstLatestClipboardContent_DoubleClick(object sender, EventArgs e)
        {
            if (lstLatestClipboardContent.SelectedItem != null)
            {
                string selectedContent = lstLatestClipboardContent.SelectedItem.ToString();


                if (selectedContent != String.Empty)
                {
                    Clipboard.SetText(selectedContent);
                }

            }

        }





        private void lstLatestClipboardContent_GotFocus(Object sender, EventArgs e)
        {
            lstLatestClipboardFiles.ClearSelected();
        }





        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }




            PictureBox selectedPictureBox = (PictureBox)sender;


            if (selectedPictureBox.Image != null)
            {
                foreach (Control control in this.Controls)
                {
                    control.Visible = false;
                }



                this.FormBorderStyle = FormBorderStyle.None;

                this.ControlBox = false;
                this.Text = "";

                this.WindowState = FormWindowState.Normal;
                

                this.Width = selectedPictureBox.Image.Width;
                this.Height = selectedPictureBox.Image.Height;

                this.BackgroundImage = selectedPictureBox.Image;


                Clipboard.Clear();

            }                
            
        }





        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox selectedPictureBox = (PictureBox)sender;


            if (selectedPictureBox.Image != null)
            {
                Clipboard.SetImage(selectedPictureBox.Image);


                foreach (PictureBox pictureBox in this.Controls.OfType<PictureBox>())
                {
                    pictureBox.BorderStyle = BorderStyle.Fixed3D;
                }


                selectedPictureBox.BorderStyle = BorderStyle.FixedSingle;
                
            }
            
        }




        private void frmMasterCopy_Click(object sender, EventArgs e)
        {
            if (this.BackgroundImage != null)
            {
                this.BackgroundImage = null;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                

                this.ControlBox = true;
                this.Text = "Master Copy";

                this.WindowState = FormWindowState.Maximized;



                foreach (Control control in this.Controls)
                {
                    control.Visible = true;
                }

            }

        }




        private void frmMasterCopy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (PictureBox pictureBox in this.Controls.OfType<PictureBox>())
                {
                    if (pictureBox.Image == null)
                    {
                        break;
                    }


                    pictureBox.BorderStyle = BorderStyle.FixedSingle;

                }



                lstLatestClipboardContent.ClearSelected();
                lstLatestClipboardFiles.ClearSelected();

                areAllImagesSelected = true;

            }




            if (e.KeyData == Keys.Delete)
            {
                if (areAllImagesSelected)
                {
                    foreach (PictureBox pictureBox in this.Controls.OfType<PictureBox>())
                    {
                        if (pictureBox.Image == null)
                        {
                            break;
                        }


                        pictureBox.Image = null;
                        pictureBox.BorderStyle = BorderStyle.Fixed3D;

                    }


                    areAllImagesSelected = false;
                    firstFreeIndex = 0;

                    Clipboard.Clear();

                }

            }

        }





        private void lstLatestClipboardFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lstLatestClipboardFiles.SelectedItem != null)
            {
                string selectedContent = lstLatestClipboardFiles.SelectedItem.ToString();


                if (selectedContent != String.Empty)
                {
                    StringCollection filesNamesToCopy = new StringCollection();
                    string[] selectedFilesNames = selectedContent.Split(',').ToArray();


                    filesNamesToCopy.AddRange(selectedFilesNames);

                    Clipboard.SetFileDropList(filesNamesToCopy);

                }

            }

        }





        private void lstLatestClipboardFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                int count = lstLatestClipboardFiles.SelectedItems.Count;


                for (int i = 0; i < count; i++)
                {
                    if (lstLatestClipboardFiles.SelectedItems[0] != null)
                    {
                        lstLatestClipboardFiles.Items.Remove(lstLatestClipboardFiles.SelectedItems[0]);
                        Clipboard.Clear();
                    }

                }

            }

        }





        private void lstLatestClipboardFiles_GotFocus(Object sender, EventArgs e)
        {
            lstLatestClipboardContent.ClearSelected();
        }






        public bool doImagesMatch(Bitmap bmp1, Bitmap bmp2)
        {
            try
            {
                //create instance or System.Drawing.ImageConverter to convert
                //each image to a byte array
                ImageConverter converter = new ImageConverter();
                //create 2 byte arrays, one for each image
                byte[] imgBytes1 = new byte[1];
                byte[] imgBytes2 = new byte[1];

                //convert images to byte array
                imgBytes1 = (byte[])converter.ConvertTo(bmp1, imgBytes2.GetType());
                imgBytes2 = (byte[])converter.ConvertTo(bmp2, imgBytes1.GetType());


                //now compute a hash for each image from the byte arrays
                SHA256Managed sha = new SHA256Managed();
                byte[] imgHash1 = sha.ComputeHash(imgBytes1);
                byte[] imgHash2 = sha.ComputeHash(imgBytes2);


                //now let's compare the hashes
                for (int i = 0; i < imgHash1.Length && i < imgHash2.Length; i++)
                {
                    //whoops, found a non-match, exit the loop
                    //with a false value
                    if (!(imgHash1[i] == imgHash2[i]))
                        return false;
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }



            //we made it this far so the images must match

            return true;

        }

    }

}
