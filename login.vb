Imports System.IO
Imports System.Data.SqlClient
Public Class login

    Dim ar As String
    Dim con As New SqlConnection("data source=" & CStr(leerarchivo(ar)) & "; initial catalog=ABM; integrated security=true")


    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        TextBox1.Text = TextBox1.Text.Replace("'", "").Replace(" ", "").Replace("""", "")

        Dim da As New SqlDataAdapter("SELECT usuario, clave from [ABM].[dbo].[Administradores] where usuario= '" & TextBox1.Text & "'and clave='" & TextBox2.Text & "'", con)
        Dim ds As New DataSet
        da.Fill(ds, "Administradores")

        If ds.Tables("Administradores").Rows.Count <> 0 Then
            If ds.Tables("Administradores").Rows(0)("clave").trim.toUpper() = TextBox2.Text.Trim.ToUpper() Then
                usuario = ds.Tables("Administradores").Rows(0)("usuario").trim
                clave = ds.Tables("Administradores").Rows(0)("clave").trim
                Dim adm As New Menu
                adm.ShowDialog()
            Else
                MsgBox("Contraseña incorrecta")
            End If
        Else
            MsgBox("Usuario no encontrado")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        TextBox1.Text = TextBox1.Text.Replace("'", "").Replace(" ", "").Replace("""", "")

        Dim da As New SqlDataAdapter("SELECT usuario, clave from [ABM].[dbo].[Clientes] where usuario= '" & TextBox1.Text & "'and clave='" & TextBox2.Text & "'", con)
        Dim ds As New DataSet
        da.Fill(ds, "Clientes")

        If ds.Tables("Clientes").Rows.Count <> 0 Then

            If ds.Tables("Clientes").Rows(0)("clave").trim.toUpper() = TextBox2.Text.Trim.ToUpper() Then
                usuario = ds.Tables("Clientes").Rows(0)("usuario").trim
                clave = ds.Tables("Clientes").Rows(0)("clave").trim
                Dim cli As New Clientes
                cli.ShowDialog()
            Else
                MsgBox("Contraseña incorrecta")
            End If

        Else
            MsgBox("Usuario no encontrado")
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        TextBox1.Text = TextBox1.Text.Replace("'", "").Replace(" ", "").Replace("""", "")

        Dim da As New SqlDataAdapter("SELECT usuario, clave from [ABM].[dbo].[Proveedores] where usuario= '" & TextBox1.Text & "'and clave='" & TextBox2.Text & "'", con)
        Dim ds As New DataSet
        da.Fill(ds, "Proveedores")

        If ds.Tables("Proveedores").Rows.Count <> 0 Then
            If ds.Tables("Proveedores").Rows(0)("clave").trim.toUpper() = TextBox2.Text.Trim.ToUpper() Then
                usuario = ds.Tables("Proveedores").Rows(0)("usuario").trim
                clave = ds.Tables("Proveedores").Rows(0)("clave").trim
                Dim prov As New Proveedores
                prov.ShowDialog()
            Else
                MsgBox("Contraseña incorrecta")
            End If
        Else
            MsgBox("Usuario no encontrado")
        End If

    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class