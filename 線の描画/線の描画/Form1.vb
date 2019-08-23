'直線の描画
'頂点はLabelコントロールで表示して、マウスドラッグで移動できるように


Public Class Form1
    Private beginPoint As Point 'マウスドラッグ開始点記録用
    Private myPen As New Pen(Brushes.Magenta, 5) '直線用のPen
    Private myPoints As New List(Of Point) '頂点座標群記録用

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call MyInitialize()
    End Sub

    '初期化
    Private Sub MyInitialize()
        '初期座標設定
        myPoints = New List(Of Point)(New Point() {
                                      New Point(0, 0),
                                      New Point(150, 40),
                                      New Point(80, 100),
                                      New Point(100, 150)})
        '頂点表示用のLabel作成
        For i = 0 To myPoints.Count - 1
            Call MakeLabel(myPoints(i), i)
        Next

        '直線を描画
        Call MyDrawLines()

    End Sub
    '左クリック時の処理
    Private Sub MyMouseDown(sender As Object, e As MouseEventArgs)
        beginPoint = e.Location
    End Sub

    'マウスドラッグ中処理
    Private Sub MyMouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim myLabel = DirectCast(sender, Label)
            Dim newLocate As Point
            newLocate = New Point(e.X - beginPoint.X + myLabel.Location.X,
                                  e.Y - beginPoint.Y + myLabel.Location.Y)
            myLabel.Location = newLocate
            myPoints(myLabel.Tag) = newLocate
            Call MyDrawLines()
        End If
    End Sub

    '直線を描画
    Private Sub MyDrawLines()
        Dim canvas As New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Dim g As Graphics = Graphics.FromImage(canvas)
        g.DrawLines(myPen, myPoints.ToArray)
        g.Dispose()
        Me.PictureBox1.Image = canvas

    End Sub
    ''' <summary>
    ''' 頂点表示用のLabel作成
    ''' </summary>
    ''' <param name="locate">頂点座標</param>
    ''' <param name="number">通し番号(何番目の頂点なのか識別用)</param>
    ''' <returns></returns>
    Private Function MakeLabel(locate As Point, number As Integer) As Point
        Dim myLabel = New Label()
        With myLabel
            .Width = 10
            .Height = 10
            .BackColor = Color.Black
            .Location = locate
            .Tag = number 'Tagに通し番号を記録
        End With
        Me.PictureBox1.Controls.Add(myLabel)
        'マウスイベント時の処理追加
        AddHandler myLabel.MouseDown, AddressOf MyMouseDown
        AddHandler myLabel.MouseMove, AddressOf MyMouseMove

    End Function

End Class
