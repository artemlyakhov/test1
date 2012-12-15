using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ChickenEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            
            //new line1


            //
            Instance = this;
            InitializeComponent();
            gameObjectsLayer1.SelectedIndex = 1;
            setting1.SelectedIndex = 0;

            InitializeBitmaps();

            SetSelection();
        }
        public static MainForm Instance = null;

        void InitializeBitmaps()
        {
            listView1.LargeImageList = new ImageList();
            listView1.SmallImageList = new ImageList();

            int i = 0;
            foreach (ResourceItem item in ResourceManager.Instance.items)
            {
                /*Rectangle rect = ResourceManager.Instance.GetRect(new Point(0,0), item.name);
                Bitmap bitmap = new Bitmap(16, 16);
                Graphics g = Graphics.FromImage(bitmap);
                ResourceManager.Instance.Draw(g, new Point(0, 0), item.name);*/
                
                listView1.LargeImageList.Images.Add(item.name, item.bitmap);
                listView1.SmallImageList.Images.Add(item.name, item.bitmap);
                listView1.Items.Add(new ListViewItem(new string[] { (i++).ToString(), item.name, item.GetStringAttribute("Setting") }, listView1.LargeImageList.Images.IndexOfKey(item.name)));
            }
        }

        private void delete1_Click(object sender, EventArgs e)
        {         
            levelControl1.DeleteItems();
        }

        private void moveUp1_Click(object sender, EventArgs e)
        {
            levelControl1.MoveItem(0, -(int)step1.Value);
        }

        private void moveDown1_Click(object sender, EventArgs e)
        {
            levelControl1.MoveItem(0, (int)step1.Value);
        }

        private void moveLeft1_Click(object sender, EventArgs e)
        {
            levelControl1.MoveItem(-(int)step1.Value, 0); //changed line
        }

        private void moveRight1_Click(object sender, EventArgs e)
        {
            levelControl1.MoveItem((int)step1.Value, 0);
        }

        private void chickenY1_CheckedChanged(object sender, EventArgs e)
        {
            if (chickenY1.Checked)
                gameObjectsLayer1.SelectedIndex = 1;
        }

        const string initialDirectory = @"c:\Users\ArtemL.PDCSPB\Documents\Chicken\Source\ChickenPC\Debug\L";
        private void save1_Click(object sender, EventArgs e)
        {
            if (sender == saveAs1)
            {
                levelControl1.filename = "";
            };         
            
            if (levelControl1.filename == "")
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.InitialDirectory = initialDirectory;
                dialog.Filter = "*.xml|*.xml";
                dialog.FileName = levelControl1.filename;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    levelControl1.filename = dialog.FileName;
                    MainForm.Instance.Text = Path.GetFileName(dialog.FileName);
                }
                else
                {
                    return;
                }
            }
            
            levelControl1.SaveXml(levelControl1.filename);

            save1.Enabled = true;
        }

        private void load1_Click(object sender, EventArgs e)
        {
            CancelEventArgs args = new CancelEventArgs();
            
            OnClosing(args);

            if (!args.Cancel)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = initialDirectory;
                dialog.Filter = "*.xml|*.xml";
                dialog.FileName = levelControl1.filename;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    levelControl1.LoadXml(dialog.FileName);
                    levelControl1.filename = dialog.FileName;
                    MainForm.Instance.Text = Path.GetFileName(dialog.FileName);
                    save1.Enabled = true;
                }
            }
        }

        public void SetSelection()
        {
            LevelItem selection = levelControl1.selections.Count == 1 ? levelControl1.selections[0] : null;

            if (selection != null)
            {
                x1.Enabled = y1.Enabled = name1.Enabled = k1.Enabled = layer1.Enabled = itemName1.Enabled = apply1.Enabled = true;

                x1.Value = selection.x;
                y1.Value = selection.y;
                name1.Text = selection.resourceName;
                k1.Value = (decimal)selection.k;
                layer1.SelectedIndex = selection.layer - 1;
                itemName1.Text = selection.itemName;

                attributes1.Items.Clear();
                foreach (LevelItem.Attribute attribute in selection.attributes)
                {
                    attributes1.Items.Add(new ListViewItem(new string[] { attribute.Name, attribute.Value }));
                }
            }
            else
            {
                x1.Value = 0;
                y1.Value = 0;
                name1.Text = "";
                k1.Value = 0;

                x1.Enabled = y1.Enabled = name1.Enabled = k1.Enabled = layer1.Enabled = itemName1.Enabled = apply1.Enabled = false;
            }

            updatingSelection = true;
            selector2.SelectedItems.Clear();
            foreach (LevelItem selectedItem in levelControl1.selections)
            {
                int index = levelControl1.items.IndexOf(selectedItem);

                foreach (ListViewItem item in selector2.Items)
                {
                    if (int.Parse(item.Text) == index)
                    {
                        selector2.SelectedIndices.Add(selector2.Items.IndexOf(item));
                        selector2.EnsureVisible(selector2.Items.IndexOf(item));
                        break;
                    }
                }                                
                
            }
            levelControl1.Focus();
            updatingSelection = false;
        }

        public void UpdateSelectorControl()
        {
            List<ListViewItem> items = new List<ListViewItem>();
            
            selector2col = 0;
            selector2sign = 1;
            selector2.ListViewItemSorter = new ListViewItemComparer(selector2col, selector2sign);
            itemUp1.Enabled = itemDown1.Enabled = itemHome1.Enabled = itemEnd1.Enabled = true;

            foreach (LevelItem levelItem in levelControl1.items)
            {
                items.Add(new ListViewItem(new string[] { 
                    levelControl1.items.IndexOf(levelItem).ToString(),
                    levelItem.itemName, levelItem.resourceName,
                    levelItem.x.ToString(),
                    levelItem.y.ToString(),
                    levelItem.layer.ToString()
                }));
            }

            selector2.Items.Clear();
            selector2.Items.AddRange(items.ToArray());

        }
        bool updatingSelection = false;
        Timer selectionChangedTimer = null;
        private void selector2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingSelection)
            {
                if (selectionChangedTimer != null)
                {
                    selectionChangedTimer.Stop();
                    selectionChangedTimer.Start();
                }
                else
                {
                    selectionChangedTimer = new Timer();
                    selectionChangedTimer.Interval = 100;
                    selectionChangedTimer.Tick += new EventHandler(delayedSelectionChangedHandler);
                    selectionChangedTimer.Start();
                }                
            }
        }

        private void delayedSelectionChangedHandler(object sender, EventArgs e)
        {
            updatingSelection = true;
            levelControl1.selections.Clear();

            foreach (ListViewItem listViewItem in selector2.SelectedItems)
            {
                int index = int.Parse(listViewItem.Text);
                levelControl1.selections.Add(levelControl1.items[index]);
            }

            SetSelection();

            updatingSelection = false;

            if (levelControl1.selections.Count > 0)
            {
                levelControl1.ScrollToItemIfNecessary(levelControl1.selections[levelControl1.selections.Count - 1]);
            }

            levelControl1.Invalidate();

            if (sender == selectionChangedTimer)
            {
                selectionChangedTimer.Stop();
                selectionChangedTimer.Dispose();
                selectionChangedTimer = null;
            }
        }   

        private void itemUp1_Click(object sender, EventArgs e) //TODO
        {
            if (selector2.SelectedIndices.Count == 1 && selector2.Items.Count > 1)
            {
                int index = selector2.SelectedIndices[0];
                int index1 = (index == 0 ? selector2.Items.Count - 1 : index - 1);
                LevelItem item1 = levelControl1.items[index1];
                LevelItem item2 = levelControl1.items[index];

                selector2.Items[index1] = new ListViewItem(new string[] { 
                    index1.ToString(),
                    item2.itemName, item2.resourceName,
                    item2.x.ToString(),
                    item2.y.ToString(),
                    item2.layer.ToString()});
                selector2.Items[index] = new ListViewItem(new string[] { 
                    index.ToString(),
                    item1.itemName, item1.resourceName,
                    item1.x.ToString(),
                    item1.y.ToString(),
                    item1.layer.ToString()});

                levelControl1.items[index1] = item2;
                levelControl1.items[index] = item1;

                selector2.SelectedIndices.Clear();
                selector2.SelectedIndices.Add(index1);
                levelControl1.Invalidate();

                levelControl1.SetModified(true);
            }
        }
        private void itemDown1_Click(object sender, EventArgs e)
        {
            if (selector2.SelectedIndices.Count == 1 && selector2.Items.Count > 1)
            {
                int index = selector2.SelectedIndices[0];
                int index1 = (index == selector2.Items.Count - 1 ? 0 : index + 1);
                LevelItem item1 = levelControl1.items[index];
                LevelItem item2 = levelControl1.items[index1];

                selector2.Items[index] = new ListViewItem(new string[] { 
                    index.ToString(),
                    item2.itemName, item2.resourceName,
                    item2.x.ToString(),
                    item2.y.ToString(),
                    item2.layer.ToString()});
                selector2.Items[index1] = new ListViewItem(new string[] { 
                    index1.ToString(),
                    item1.itemName, item1.resourceName,
                    item1.x.ToString(),
                    item1.y.ToString(),
                    item1.layer.ToString()});                

                levelControl1.items[index] = item2;
                levelControl1.items[index1] = item1;

                selector2.SelectedIndices.Clear();
                selector2.SelectedIndices.Add(index1);
                levelControl1.Invalidate();

                levelControl1.SetModified(true);
            }
        }

        private void itemHome1_Click(object sender, EventArgs e)
        {
            if (selector2.SelectedIndices.Count == 1 && selector2.SelectedIndices[0] > 0)
            {
                LevelItem item = levelControl1.items[selector2.SelectedIndices[0]];
                levelControl1.items.Remove(item);
                levelControl1.items.Insert(0, item);

                selector2.Items.RemoveAt(selector2.SelectedIndices[0]);
                selector2.Items.Insert(0, new ListViewItem(new string[] { 
                    (0).ToString(),
                    item.itemName, item.resourceName,
                    item.x.ToString(),
                    item.y.ToString(),
                    item.layer.ToString()}));
                selector2.SelectedIndices.Clear();
                selector2.SelectedIndices.Add(0);

                //fix indexes
                int i = 0;
                foreach (ListViewItem lvItem in selector2.Items)
                {
                    lvItem.SubItems[0].Text = i.ToString();
                    i++;
                }

                levelControl1.Invalidate();
                levelControl1.SetModified(true);
            }
        }

        private void itemEnd1_Click(object sender, EventArgs e)
        {
            if (selector2.SelectedIndices.Count == 1 && selector2.SelectedIndices[0] < selector2.Items.Count - 1)
            {
                updatingSelection = true;

                LevelItem item = levelControl1.items[selector2.SelectedIndices[0]];
                levelControl1.items.Remove(item);
                levelControl1.items.Add(item);

                selector2.Items.RemoveAt(selector2.SelectedIndices[0]);
                selector2.Items.Add(new ListViewItem(new string[] { 
                    (selector2.Items.Count+1).ToString(),
                    item.itemName, item.resourceName,
                    item.x.ToString(),
                    item.y.ToString(),
                    item.layer.ToString()}));
                selector2.SelectedIndices.Clear();
                selector2.SelectedIndices.Add(selector2.Items.Count - 1);

                //fix indexes
                int i = 0;
                foreach (ListViewItem lvItem in selector2.Items)
                {
                    lvItem.SubItems[0].Text = i.ToString();
                    i++;
                }

                levelControl1.Invalidate();
                levelControl1.SetModified(true);

                updatingSelection = false;
            }
        }



        private void apply1_Click(object sender, EventArgs e)
        {
            if (levelControl1.selections.Count == 1)
            {
                LevelItem selection = levelControl1.selections[0];
                selection.x = (int)x1.Value;
                selection.y = (int)y1.Value;
                selection.resourceName = name1.Text;
                selection.k = (float)k1.Value;
                selection.layer = layer1.SelectedIndex + 1;

                levelControl1.Invalidate();
            }

        }

        private void repeat0_Click(object sender, EventArgs e)
        {
            levelControl1.RepeatItem((int)repeat1.Value);
        }

        private void displayGrid1_CheckedChanged(object sender, EventArgs e)
        {
            levelControl1.displayGrid = displayGrid1.Checked;
            levelControl1.Invalidate();
        }

        private void snapToGrid1_CheckedChanged(object sender, EventArgs e)
        {
            levelControl1.snapToGrid = snapToGrid1.Checked;
            levelControl1.Invalidate();
        }

        private void gridSize1_ValueChanged(object sender, EventArgs e)
        {
            levelControl1.gridSize = (int)gridSize1.Value;
            levelControl1.Invalidate();
        }

        private void displayChicken1_CheckedChanged(object sender, EventArgs e)
        {
            levelControl1.Invalidate();
        }

        private void addAttribute1_Click(object sender, EventArgs e)
        {
            if (levelControl1.selections.Count == 1)
            {
                AttributeDialog dlg = new AttributeDialog();
                dlg.ShowDialog(this);
                if (dlg.DialogResult == DialogResult.OK)
                {
                    levelControl1.selections[0].attributes.Add(new LevelItem.Attribute(dlg.Name, dlg.Value));
                    
                    SetSelection();
                }

            }
        }

        private void removeAttribute1_Click(object sender, EventArgs e)
        {
            if (levelControl1.selections.Count == 1 && attributes1.SelectedIndices.Count == 1)
            {
                levelControl1.selections[0].attributes.RemoveAt(attributes1.SelectedIndices[0]);
                
                SetSelection();
            }
        }

        private void fontBuilder1_Click(object sender, EventArgs e)
        {
            (new MFontBuilder()).Build();

            MessageBox.Show("Done");
        }

        private void x1Resource1_Click(object sender, EventArgs e)
        {
            (new X1ResourceConverter()).Convert();

            MessageBox.Show("Done");
        }

        int listView1col = 0;
        int listView1sign = 1;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listView1sign = e.Column == listView1col ? -listView1sign : 1;
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1sign);
            listView1col = e.Column;
        }
        int selector2col = 0;
        int selector2sign = 1;
        private void selector2_ColumnClick(object sender, ColumnClickEventArgs e)
        {         
            selector2sign = e.Column == selector2col ? -selector2sign : 1;
            selector2.ListViewItemSorter = new ListViewItemComparer(e.Column, selector2sign);
            selector2col = e.Column;

            bool movementsEnables = e.Column == 0 && selector2sign == 1;
            itemUp1.Enabled = itemDown1.Enabled = itemHome1.Enabled = itemEnd1.Enabled = movementsEnables;
        }

        private void selectLayer1_CheckedChanged(object sender, EventArgs e)
        {
            levelControl1.Invalidate();
        }

        private void selector2_KeyDown(object sender, KeyEventArgs e)
        {
            levelControl1.PublicOnKeyDown(e);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (levelControl1.modified)
            {
                DialogResult result = MessageBox.Show("Save modified level?", "Question", MessageBoxButtons.YesNoCancel);

                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    save1_Click(save1, new EventArgs());
                }
            }
        }

        private void width1_ValueChanged(object sender, EventArgs e)
        {
            levelControl1.Width -= 10;
            levelControl1.Width += 10;
        }
    }

    // Implements the manual sorting of items by columns.
    class ListViewItemComparer : IComparer
    {
        private int col, sign;
        public ListViewItemComparer(int column, int sign)
        {
            col = column;
            this.sign = sign;
        }
        public int Compare(object x, object y)
        {
            int ix, iy;
            if (int.TryParse(((ListViewItem)x).SubItems[col].Text, out ix) && int.TryParse(((ListViewItem)y).SubItems[col].Text, out iy))
            {
                return sign * (ix > iy ? 1 : (ix < iy ? -1 : 0));
            }            
            else
            {
                return sign*String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
    }



}