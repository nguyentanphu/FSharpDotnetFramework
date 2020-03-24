open System
open System.Drawing
open System.Windows.Forms

let random = new Random()

let randomBrush() = 
    let r,g,b = random.Next(256), random.Next(256), random.Next(256)
    new SolidBrush(Color.FromArgb(r, g, b))

let drawPieSegment (graphic: Graphics) label startAngel angle = 
    graphic.FillPie(randomBrush(), 170, 70, 260, 260, startAngel, angle)

let main = new Form(Width = 620, Height = 450, Text = "Pie Chart")
let menu = new ToolStrip()
let btnOpen = new ToolStripButton("Open")
let btnSave = new ToolStripButton("Save", Enabled = false)
menu.Items.Add(btnOpen) |> ignore
menu.Items.Add(btnSave) |> ignore
let img =
 new PictureBox(BackColor = Color.White, Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.CenterImage)
main.Controls.Add(menu)
main.Controls.Add(img)
// TODO: Drawing of the chart & user interface interactions #2
[<STAThread>]
do
 Application.Run(main) 
