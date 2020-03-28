open System
open System.IO
open System.Drawing
open System.Windows.Forms


let convertRowData (str:string) = 
    let cells = List.ofSeq(str.Split ',')
    match cells with
    | label::num::tail -> 
        let intNum = Int32.Parse num
        (label, intNum)
    | _ -> failwith "Incorrect data format"

let rec processLines lines = 
    match lines with
    | [] -> []
    | str::tail -> 
        let typle = convertRowData str
        typle::processLines tail

let rec countSum rows = 
    match rows with
    | [] -> 0
    | (_, n)::tail -> n + countSum tail



let random = new Random()

let randomBrush() = 
    let r,g,b = random.Next(256), random.Next(256), random.Next(256)
    new SolidBrush(Color.FromArgb(r, g, b))

let drawPieSegment (graphic: Graphics) label startAngel angle = 
    graphic.FillPie(randomBrush(), 170, 70, 260, 260, startAngel, angle)

let drawStep drawingFunc (graphic: Graphics) sum data = 
    let rec drawStepUntil data currentAngel = 
        match data with
        | [] -> ()
        | [label, num] ->
            let newAngel = 360 - currentAngel
            drawingFunc graphic label currentAngel newAngel
        | (label, num)::tail ->
            let newAngel = int(float (num) / sum * 360.0)
            drawingFunc graphic label currentAngel newAngel
            drawStepUntil tail (currentAngel + newAngel)
    drawStepUntil data 0

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
let drawPieChart filePath =
    let lines = List.ofSeq(File.ReadAllLines filePath)
    let data = processLines(lines)
    let bmp = new Bitmap(600, 400)
    let graphic = Graphics.FromImage bmp
    graphic.FillRectangle(Brushes.White, 0, 0, 600, 400)
    let sum = float (countSum data)
    drawStep drawPieSegment graphic sum data
    bmp
[<STAThread>]
do
 Application.Run(main) 
