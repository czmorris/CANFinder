using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace CANFinder
{
    public partial class Form1 : Form
    {

        uint[] arrIDs;
        byte[] arrLENs;
        byte[] arrD0;
        byte[] arrD1;
        byte[] arrD2;
        byte[] arrD3;
        byte[] arrD4;
        byte[] arrD5;
        byte[] arrD6;
        byte[] arrD7;

        uint[] arrInvIDs;
        int cntIndIDs;     // The count of the individual ids.



        public Form1()
        {
            InitializeComponent();

            arrIDs  = new uint[100000];
            arrLENs = new byte[100000];
            arrD0   = new byte[100000];
            arrD1   = new byte[100000];
            arrD2   = new byte[100000];
            arrD3   = new byte[100000];
            arrD4   = new byte[100000];
            arrD5   = new byte[100000];
            arrD6   = new byte[100000];
            arrD7   = new byte[100000];
            arrInvIDs = new uint[100];   // At most 100 individual ids. Might have to change but should be good enough.

            cntIndIDs = 0;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

            if(res == DialogResult.OK)
            {
                txtLogFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            int LinePos = 0;
            cntIndIDs = 0;

            using (TextFieldParser csvParser = new TextFieldParser(txtLogFilePath.Text))
            {
                csvParser.SetDelimiters(new string[] { " " });
                csvParser.HasFieldsEnclosedInQuotes = false;

                // Skip the header line
                csvParser.ReadLine();

                while(!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    arrIDs[LinePos] = Convert.ToUInt32(fields[1], 16);

                    // Check to see if we have it already. If not add it to the individual ids list. 
                    for(int i = 0; i <= cntIndIDs; i++)
                    {
                        // If we already have this value then break and leave loop.
                        if(arrIDs[LinePos] == arrInvIDs[i])
                        { break; }

                        // Made it to the end and no match found.
                        // Add it and leave the loop.
                        if(i == cntIndIDs)
                        { 
                            arrInvIDs[cntIndIDs] = arrIDs[LinePos];
                            cntIndIDs++;
                            break;
                        }
                    }

                    arrLENs[LinePos] = Convert.ToByte(fields[2], 16);
                    arrD0[LinePos] = Convert.ToByte(fields[3], 16);

                    if(arrLENs[LinePos] > 1) 
                    { arrD1[LinePos] = Convert.ToByte(fields[4], 16); }
                    else { arrD1[LinePos] = 0; }

                    if (arrLENs[LinePos] > 2)
                    { arrD2[LinePos] = Convert.ToByte(fields[5], 16); }
                    else { arrD2[LinePos] = 0; }

                    if (arrLENs[LinePos] > 3)
                    { arrD3[LinePos] = Convert.ToByte(fields[6], 16); }
                    else { arrD3[LinePos] = 0; }

                    if (arrLENs[LinePos] > 4)
                    { arrD4[LinePos] = Convert.ToByte(fields[7], 16); }
                    else { arrD4[LinePos] = 0; }

                    if (arrLENs[LinePos] > 5)
                    { arrD5[LinePos] = Convert.ToByte(fields[8], 16); }
                    else { arrD5[LinePos] = 0; }

                    if (arrLENs[LinePos] > 6)
                    { arrD6[LinePos] = Convert.ToByte(fields[9], 16); }
                    else { arrD6[LinePos] = 0; }

                    if (arrLENs[LinePos] > 7)
                    { arrD7[LinePos] = Convert.ToByte(fields[10], 16); }
                    else { arrD7[LinePos] = 0; }

                    LinePos++;
                }

                
            }

            UpdListView();  // Try to update the listview of IDs.

        }

        // Try to update the left list of ids. 
        private void UpdListView()
        {
            listIDs.Items.Clear();

            for(int i = 0; i < cntIndIDs; i++)
            {
                //listIDs.Items.Add(arrInvIDs[i]);
                listIDs.Items.Add(String.Format("[0x{0:X}] d{1}", arrInvIDs[i], arrInvIDs[i]));
            }

        }

        // The user has changed the ID selection in the listbox. 
        // Reload the screen to reflect the new ID.
        private void listIDs_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillDgvWithId(arrInvIDs[listIDs.SelectedIndex]);

        }

        private void FillDgvWithId(uint id)
        {
            uint rowid = 0;
            byte len = 0;
            byte d0 = 0;
            byte d1 = 0;
            byte d2 = 0;
            byte d3 = 0;
            byte d4 = 0;
            byte d5 = 0;
            byte d6 = 0;
            byte d7 = 0;

            bool d0changed = false;
            bool d1changed = false;
            bool d2changed = false;
            bool d3changed = false;
            bool d4changed = false;
            bool d5changed = false;
            bool d6changed = false;
            bool d7changed = false;

            string[] arrCol = new string[10];

            // Populate the datagridview. 
            dgvMessages.Rows.Clear();
            dgvMessages.ColumnCount = 10;

            dgvMessages.Rows.Add("ID", "LEN", "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7");

            // For all available messages.
            for(int i = 0; i < arrIDs.Length; i++)
            { 
                // Does this id match the one we are working with?
                if(id == arrIDs[i])
                {
                    rowid = id;
                    len   = arrLENs[i];

                    // Did the data byte change at all?
                    if ((i > 0) && (d0 != arrD0[i])) { d0changed = true; }
                    if ((i > 0) && (d1 != arrD1[i])) { d1changed = true; }
                    if ((i > 0) && (d2 != arrD2[i])) { d2changed = true; }
                    if ((i > 0) && (d3 != arrD3[i])) { d3changed = true; }
                    if ((i > 0) && (d4 != arrD4[i])) { d4changed = true; }
                    if ((i > 0) && (d5 != arrD5[i])) { d5changed = true; }
                    if ((i > 0) && (d6 != arrD6[i])) { d6changed = true; }
                    if ((i > 0) && (d7 != arrD7[i])) { d7changed = true; }

                    d0    = arrD0[i];
                    if (len >= 2) { d1 = arrD1[i]; }
                    if (len >= 3) { d2 = arrD2[i]; }
                    if (len >= 4) { d3 = arrD3[i]; }
                    if (len >= 5) { d4 = arrD4[i]; }
                    if (len >= 6) { d5 = arrD5[i]; }
                    if (len >= 7) { d6 = arrD6[i]; }
                    if (len >= 8) { d7 = arrD7[i]; }


                    // Note: It might be have blanks where length does not extend for a message... 
                    arrCol[0] = String.Format("0x{0:X8}", rowid);
                    arrCol[1] = String.Format("{0}", len);
                    arrCol[2] = String.Format("0x{0:X2}", d0);
                    arrCol[3] = String.Format("0x{0:X2}", d1);
                    arrCol[4] = String.Format("0x{0:X2}", d2);
                    arrCol[5] = String.Format("0x{0:X2}", d3);
                    arrCol[6] = String.Format("0x{0:X2}", d4);
                    arrCol[7] = String.Format("0x{0:X2}", d5);
                    arrCol[8] = String.Format("0x{0:X2}", d6);
                    arrCol[9] = String.Format("0x{0:X2}", d7);

                    dgvMessages.Rows.Add(arrCol);
                }
            }

            // If the column had any changes at all then highlight the column.
            if (d0changed) { dgvMessages.Columns[2].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[2].DefaultCellStyle.BackColor = Color.White; }

            if (d1changed) { dgvMessages.Columns[3].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[3].DefaultCellStyle.BackColor = Color.White; }

            if (d2changed) { dgvMessages.Columns[4].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[4].DefaultCellStyle.BackColor = Color.White; }

            if (d3changed) { dgvMessages.Columns[5].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[5].DefaultCellStyle.BackColor = Color.White; }

            if (d4changed) { dgvMessages.Columns[6].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[6].DefaultCellStyle.BackColor = Color.White; }

            if (d5changed) { dgvMessages.Columns[7].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[7].DefaultCellStyle.BackColor = Color.White; }

            if (d6changed) { dgvMessages.Columns[8].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[8].DefaultCellStyle.BackColor = Color.White; }

            if (d7changed) { dgvMessages.Columns[9].DefaultCellStyle.BackColor = Color.Coral; }
            else { dgvMessages.Columns[9].DefaultCellStyle.BackColor = Color.White; }

        }
    }
}
