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
using System.IO;

namespace CANFinder
{
    public partial class Form1 : Form
    {
        float[] seconds;
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

        uint totalmsgcnt;



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
            seconds = new float[100000];  // all the memory be gone... 
            arrInvIDs = new uint[100];   // At most 100 individual ids. Might have to change but should be good enough.

            cntIndIDs = 0;
            totalmsgcnt = 0;

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
            totalmsgcnt = 0;
            float InitialSeconds = 0.0F;
            ulong tempmicrosec = 0;

            using (TextFieldParser csvParser = new TextFieldParser(txtLogFilePath.Text))
            {
                csvParser.SetDelimiters(new string[] { " " });
                csvParser.HasFieldsEnclosedInQuotes = false;

                // Skip the header line
                csvParser.ReadLine();

                while(!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();

                    totalmsgcnt++;

                    tempmicrosec = Convert.ToUInt64(fields[0]);

                    // Grab the initial timestamp
                    if (LinePos == 0)
                    {
                        InitialSeconds = (float)(tempmicrosec / 1000000.0);
                    }

                    // Deduct the initial timestamp and store.
                    seconds[LinePos] = (float)((tempmicrosec / 1000000.0) - InitialSeconds);

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

            int idcnts = 0;
            int columncnt = 0;

            string[] arrCol = new string[10];

            // Populate the datagridview. 
            dgvMessages.Rows.Clear();
            dgvMessages.ColumnCount = 10;

            //dgvMessages.Rows.Add("ID", "LEN", "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7");

            foreach (DataGridViewColumn column in dgvMessages.Columns)
            {


                switch(columncnt)
                {
                    case 0: // ID
                        column.HeaderText = "ID";
                        break;
                    case 1: // length
                        column.HeaderText = "LEN";
                        break;
                    case 2: // d0
                        column.HeaderText = "D0";
                        break;
                    case 3: // d1
                        column.HeaderText = "D1";
                        break;
                    case 4: // d2
                        column.HeaderText = "D2";
                        break;
                    case 5: // d3
                        column.HeaderText = "D3";
                        break;
                    case 6: // d4
                        column.HeaderText = "D4";
                        break;
                    case 7: // d5
                        column.HeaderText = "D5";
                        break;
                    case 8: // d6
                        column.HeaderText = "D6";
                        break;
                    case 9: // d7
                        column.HeaderText = "D7";
                        break;

                }

                columncnt++;
            }

            // For all available messages.
            for (int i = 0; i < totalmsgcnt; i++)
            { 
                // Does this id match the one we are working with?
                if(id == arrIDs[i])
                {
                    idcnts++;

                    rowid = id;
                    len   = arrLENs[i];

                    // Did the data byte change at all?
                    if ((idcnts > 1) && (d0 != arrD0[i])) 
                    { d0changed = true; }
                    if ((idcnts > 1) && (d1 != arrD1[i])) 
                    { d1changed = true; }
                    if ((idcnts > 1) && (d2 != arrD2[i])) 
                    { d2changed = true; }
                    if ((idcnts > 1) && (d3 != arrD3[i])) 
                    { d3changed = true; }
                    if ((idcnts > 1) && (d4 != arrD4[i])) 
                    { d4changed = true; }
                    if ((idcnts > 1) && (d5 != arrD5[i])) 
                    { d5changed = true; }
                    if ((idcnts > 1) && (d6 != arrD6[i])) 
                    { d6changed = true; }
                    if ((idcnts > 1) && (d7 != arrD7[i])) 
                    { d7changed = true; }

                    // Update the locals with values from the primary array.
                    d0    = arrD0[i];
                    if (len >= 2) { d1 = arrD1[i]; }
                    if (len >= 3) { d2 = arrD2[i]; }
                    if (len >= 4) { d3 = arrD3[i]; }
                    if (len >= 5) { d4 = arrD4[i]; }
                    if (len >= 6) { d5 = arrD5[i]; }
                    if (len >= 7) { d6 = arrD6[i]; }
                    if (len >= 8) { d7 = arrD7[i]; }

                    if(chkShowHex.Checked)
                    {
                        // Format the value for each column. Include hex.
                        arrCol[0] = String.Format("0x{0:X8}", rowid);
                        arrCol[1] = String.Format("{0}", len);
                        arrCol[2] = String.Format("0x{0:X2}[{1}]", d0, d0);
                        arrCol[3] = String.Format("0x{0:X2}[{1}]", d1, d1);
                        arrCol[4] = String.Format("0x{0:X2}[{1}]", d2, d2);
                        arrCol[5] = String.Format("0x{0:X2}[{1}]", d3, d3);
                        arrCol[6] = String.Format("0x{0:X2}[{1}]", d4, d4);
                        arrCol[7] = String.Format("0x{0:X2}[{1}]", d5, d5);
                        arrCol[8] = String.Format("0x{0:X2}[{1}]", d6, d6);
                        arrCol[9] = String.Format("0x{0:X2}[{1}]", d7, d7);
                    }
                    else
                    {
                        // Format the value for each column. Decimal only.
                        arrCol[0] = String.Format("{0}", rowid);
                        arrCol[1] = String.Format("{0}", len);
                        arrCol[2] = String.Format("{0}", d0);
                        arrCol[3] = String.Format("{0}", d1);
                        arrCol[4] = String.Format("{0}", d2);
                        arrCol[5] = String.Format("{0}", d3);
                        arrCol[6] = String.Format("{0}", d4);
                        arrCol[7] = String.Format("{0}", d5);
                        arrCol[8] = String.Format("{0}", d6);
                        arrCol[9] = String.Format("{0}", d7);
                    }


                    dgvMessages.Rows.Add(arrCol);
                    dgvMessages.Rows[idcnts-1].HeaderCell.Value = (idcnts).ToString(); // Start from 1 not 0
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

        private void chkShowHex_CheckedChanged(object sender, EventArgs e)
        {
            FillDgvWithId(arrInvIDs[listIDs.SelectedIndex]);
        }

        // 
        private void btnSaveParsed_Click(object sender, EventArgs e)
        {
            DialogResult drslt;

            drslt = dlgSaveParse.ShowDialog();

            if(drslt == DialogResult.OK)
            {
                SaveParsed(dlgSaveParse.FileName);
            }

        }

        void SaveParsed(string path)
        {
            float timestamp;         // In seconds
            float Soc = 0.0F;        // %
            float Amps = 0.0F;       // in Ampheres.
            int   GearMode = 0;      // Enum
            string GMString = "";
            float Odo = 0.0F;        // in miles
            float Trip = 0.0F;       // in miles
            float Speed = 0.0F;      // in mph
            float CtrlTemp = 0.0F;   // Controller temp in C
            int   SideStand = 0;     // Side Stand Status

            float LastSocChange = 0.0F;
            float TripAtSocChange = 0.0F;
            float PercentPerMile = 0.0F;
            float SumForAvgPPM = 0.0F;
            int CountAvgPPM = 0;
            float AvgPPM = 0.0F;

            float BatteryVoltage = 0.0F;  // This is a test until proven...
            float BatteryWatts = 0.0F;    // Test until proven. May not include items powered by DC-DC.

            string line;

            bool FirstSocIn = false;

            StreamWriter file = new StreamWriter(path);

            line = "Seconds, SoC(%), Amps, GearMode, Odometer(miles), Trip(miles), Speed(mph), Contr. Temp (C), SideStand (1/0), %SoCPPM, AvgPPM, BATVolts, BATWatts";
            file.WriteLine(line);  // Write the header line.

            // For every message. 
            for(int i = 0; i < totalmsgcnt; i++)
            {
                timestamp = seconds[i]; 

                // Update any values that need it.
                switch(arrIDs[i])
                {
                    case 0x19: // BMS (Soc/Amps)
                        Soc = arrD1[i];
                        Amps = (float)((arrD6[i] + (255 * arrD7[i])) / 100.0);

                        // Always update on the first to get started...
                        if ((!FirstSocIn) && (Soc > 0))
                        {
                            LastSocChange = Soc;
                            TripAtSocChange = Trip;
                            FirstSocIn = true;
                        }
                        else if ((FirstSocIn) && (Soc != LastSocChange))
                        {
                            // Avoid divide by zero. Must have enough valid trip data.
                            if ((Trip - TripAtSocChange > 0) && (Soc != LastSocChange) && (Soc > 0))
                            {
                                PercentPerMile = ((LastSocChange - Soc) / (Trip - TripAtSocChange));
                                LastSocChange = Soc;
                                TripAtSocChange = Trip;

                                SumForAvgPPM += PercentPerMile;
                                CountAvgPPM++;


                                // Avoid divide by zero. Don't try to average without any samples
                                if(CountAvgPPM > 0)
                                {
                                    AvgPPM = (SumForAvgPPM / (float)CountAvgPPM);
                                }
                                else
                                {
                                    AvgPPM = 0;
                                }
                            }
                        }

                        break;
                    case 0x619:

                        BatteryVoltage = (float)( ((arrD3[i] * 255) + arrD2[i]) / 100.0 );

                        break;
                    case 0xA0:  // GearMode
                        GearMode = arrD0[i];

                        switch(GearMode)
                        {
                            case 0x01: // ECO
                                GMString = "ECO";
                                break;
                            case 0x02: // POWER
                                GMString = "POWER";
                                break;
                            case 0x03: // REV
                                GMString = "REVERSE";
                                break;
                            case 0x17: // PARK
                                GMString = "PARK";
                                break;
                        }

                        break;
                    case 0x2D0: // Odo/Trip
                        Odo = (float)(((255.0 * arrD3[i]) + arrD2[i]) / 1.609);             // Odo in kph converted to miles.
                        Trip = (float)((((255.0 * arrD6[i]) + arrD5[i]) / 10.0 ) / 1.609);  // trip in kph converted to miles.
                        break;
                    case 0x101: // Speed/SS
                        Speed = (float)((arrD1[i] * 1.275) / 1.609);
                        SideStand = (arrD5[i] & 0x80) >> 7;
                        CtrlTemp = (float)(arrD6[i] - 40);
                        break;
                }

                // Volts and Amps provided in different CAN messages. So calculated outside of switch.
                BatteryWatts = (BatteryVoltage * Amps);


                // now build the line to be written to the file
                line = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                    timestamp,
                    Soc,
                    Amps,
                    GearMode,
                    Odo,
                    Trip,
                    Speed,
                    CtrlTemp,
                    SideStand,
                    PercentPerMile,
                    AvgPPM,
                    BatteryVoltage,
                    BatteryWatts);

                file.WriteLine(line);  // Write this row

            }

            file.Close();
            MessageBox.Show("Done!");  // Note.. Should change to check for errors.
        }


    }
}
