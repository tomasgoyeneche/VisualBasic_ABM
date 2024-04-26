Public Class Menu

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cli As New Clientes
        cli.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cli As New Proveedores
        cli.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim cli As New Administradores
        cli.ShowDialog()
    End Sub

End Class