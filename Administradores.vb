Imports System.IO
Imports System.Data.SqlClient

Public Class Administradores

    Dim ar As String
    Dim con As New SqlConnection("data source=" & CStr(leerarchivo(ar)) & "; initial catalog=ABM; integrated security=true")

    Function leerarchivo(ByVal archivo As String) As String
        If File.Exists("c:\ABM\ip.txt") = True Then
            Dim SR As StreamReader = File.OpenText("c:\ABM\ip.txt")
            Dim Line As String = SR.ReadLine()
            SR.Close()
            Return Line
        Else
            MsgBox("Verifique Falta Archivo de Configuracion")
            Me.Close()
        End If
    End Function

    Private Sub ABM_Administradores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buscar(" apellido like '" & tApellido.Text & "%' ")
    End Sub
    Sub buscar(ByVal condicion As String)

        Dim da As New SqlDataAdapter("SELECT TOP (100) PERCENT NAdmi, apellido from [ABM].[dbo].[Administradores] where " & condicion & " order by apellido", con)
        Dim ds As New DataSet
        da.Fill(ds, "Administradores")
        If ds.Tables("Administradores").Rows.Count = 0 Then

            DataGridView1.Visible = False

            pCampos.Visible = False
            lLegajo.Visible = False
        Else

            DataGridView1.DataSource = ds.Tables("Administradores")
            DataGridView1.Refresh()
            DataGridView1.Visible = True

            lLegajo.Visible = True
        End If

    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        FilaClick(e)
    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        FilaClick(e)
    End Sub
    Sub FilaClick(ByVal e As Object)
        Dim fila As Integer = e.RowIndex
        Dim tfila As String

        If IsNothing(DataGridView1.Rows(fila).Cells(0).Value) Then
            lLegajo.Text = "0"

            pCampos.Visible = False
            Exit Sub
        Else
            tfila = DataGridView1.Rows(fila).Cells(0).Value
            lLegajo.Text = tfila.ToString()
            CargarCamposAdministradores()
        End If

    End Sub

    Sub CargarCamposAdministradores()
        If Val(lLegajo.Text) = 0 Then

            pCampos.Visible = False

            Exit Sub
        Else

            pCampos.Visible = True
            Dim da As New SqlDataAdapter("SELECT  upper(ltrim(rtrim(isnull(apellido,'****')))) as apellido, upper(ltrim(rtrim(isnull(nombre,'****')))) as nombre,isnull([documento],0) as doc, ltrim(rtrim(isnull(domicilio,''))) as dirección,ltrim(rtrim(isnull(localidad,''))) as localidad,ltrim(rtrim(isnull(provincia,''))) as provincia,ltrim(rtrim(isnull(teléfono,''))) as teléfono,Fechanacimiento as fechanacimiento,ltrim(rtrim(isnull(comentarios,''))) as comentarios,ltrim(rtrim(isnull([EMail],''))) as email, isnull(estado,0) as Estado, upper(ltrim(rtrim(isnull(cuit,'****')))) as cuit, upper(ltrim(rtrim(isnull(usuario,'****')))) as usuario, upper(ltrim(rtrim(isnull(clave,'****')))) as clave from [ABM].[dbo].[Administradores] where NAdmi=" & Val(lLegajo.Text), con)
            Dim ds As New DataSet
            da.Fill(ds, "Administradores")
            TextBox1.Text = ds.Tables("Administradores").Rows(0)("apellido")
            TextBox2.Text = ds.Tables("Administradores").Rows(0)("nombre")
            TextBox3.Text = ds.Tables("Administradores").Rows(0)("doc")

            TextBox4.Text = ds.Tables("Administradores").Rows(0)("Dirección")
            TextBox5.Text = ds.Tables("Administradores").Rows(0)("localidad")
            TextBox8.Text = ds.Tables("Administradores").Rows(0)("provincia")
            TextBox6.Text = ds.Tables("Administradores").Rows(0)("teléfono")
            TextBox7.Text = ds.Tables("Administradores").Rows(0)("email")
            CheckBox1.Checked = IIf(ds.Tables("Administradores").Rows(0)("estado") = 0, False, True)


            TextBox12.Text = ds.Tables("Administradores").Rows(0)("comentarios")

            DateTimePicker1.Value = ds.Tables("Administradores").Rows(0)("fechanacimiento")

            TextBox9.Text = ds.Tables("Administradores").Rows(0)("cuit")
            TextBox10.Text = ds.Tables("Administradores").Rows(0)("usuario")
            TextBox11.Text = ds.Tables("Administradores").Rows(0)("clave")
        End If
    End Sub

    Private Sub bBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscar.Click
        buscar(" apellido like '" & tApellido.Text & "%' ")
    End Sub

    Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click
        tApellido.Text = ""
        buscar(" apellido like '" & tApellido.Text & "%' ")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If MessageBox.Show("Está por ELIMINAR definitivamente al Administrador: " & TextBox1.Text.Trim.ToUpper & ", " & TextBox12.Text.Trim.ToUpper & ". Es algo EXTREMO, porque se perderán las referencias. Está SEGURO?", "Eliminar Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub

        If SQL_Accion("delete from [ABM].[dbo].[Administradores]  where  NAdmi=" & Val(lLegajo.Text)) = False Then
            MsgBox("Hubo un error al intentar borrar al Administrador, reintente, y si el error persiste, anote todos los datos que quizo ingresar y comuníquese con el programador.")
        Else

            buscar(" NAdmi=" & Val(lLegajo.Text))
            MsgBox("El Administrador fue ELIMINADO de la base de datos.")

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim errores As String = "", en As String = vbCrLf
        If TextBox1.Text.Trim.Length < 3 Then
            errores &= "Debe completar el Apellido del Administrador." & en
        End If
        If TextBox2.Text.Trim.Length < 3 Then
            errores &= "Debe completar el o los nombres del Administrador." & en
        End If
        TextBox3.Text = Val(TextBox3.Text.Trim.Replace(".", "").Replace(" ", "").Replace(",", ""))
        If TextBox3.Text.Trim.Length < 4 Or TextBox3.Text.IndexOf("11111") > -1 Or TextBox3.Text.IndexOf("12345") > -1 Or TextBox3.Text.IndexOf("000000") > -1 Then
            errores &= "Debe completar CORRECTAMENTE el documento del Administrador." & en
        End If
        If TextBox7.Text.Trim.Length <> 0 And (TextBox7.Text.IndexOf("@") < 0 Or TextBox7.Text.IndexOf(".") < 0) Then
            errores &= "Verifique por favor el email del Administrador. No es obligatorio, pero si lo escribe debe ser correcto." & en
        End If
        If errores.Length > 0 Then
            MsgBox("Hubo errores, por favor verifique y corrija antes de intentar de nuevo:" & en & en & errores)
            Exit Sub
        End If

        If SQL_Accion("update [ABM].[dbo].[Administradores] set estado=" & IIf(CheckBox1.Checked, 1, 0) & ", apellido='" & TextBox1.Text.Trim.ToUpper.Replace("'", "´") & "', nombre='" & TextBox2.Text.Trim.ToUpper.Replace("'", "´") & "', [documento]=" & Val(TextBox3.Text.Trim.Replace(".", "").Replace(" ", "").Replace(",", "")) & ", domicilio='" & TextBox4.Text.Trim.ToUpper.Replace("'", "´") & "', localidad='" & TextBox5.Text.Trim.ToUpper.Replace("'", "´") & "', provincia='" & TextBox8.Text.Trim.ToUpper.Replace("'", "´") & "', teléfono='" & TextBox6.Text.Trim.ToUpper.Replace("'", "´") & "', [email]='" & TextBox7.Text.Trim.ToUpper.Replace("'", "´") & "', fechanacimiento=" & FechaSQL(DateTimePicker1.Value) & ", comentarios='" & TextBox12.Text.Trim.ToUpper.Replace("'", "´") & "', cuit='" & TextBox9.Text.Trim.ToUpper.Replace("'", "´") & "', usuario='" & TextBox10.Text.Trim.ToUpper.Replace("'", "´") & "', clave='" & TextBox11.Text.Trim.ToUpper.Replace("'", "´") & "' where NAdmi=" & VNum(lLegajo.Text)) = True Then
            MsgBox("Cambios realizados correctamente.")

            buscar(" NAdmi=" & VNum(lLegajo.Text))
        Else
            MsgBox("Se produjo un error al querer guardar los datos del Administrador, reintente, y si el error persiste, anote todos los datos que quizo ingresar y comuníquese con el programador.")
        End If
    End Sub

    Private Sub bNuevoAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bNuevoAdmin.Click
        If SQL_Accion("insert into [ABM].[dbo].[Administradores] (apellido, nombre, [documento],domicilio, localidad, teléfono, fechanacimiento, comentarios,[email],estado, cuit, usuario, clave) values ('*****','*****',                  0,           '',           '',             '',           getdate(),               ''       ,      ''    ,     1 , '', '', ''  )  ") Then


            buscar(" apellido like '****%' ")
            MsgBox("Se ha creado un nuevo registro para el Administrador que desea ingresar, seleccione la línea nueva, cargue los datos y luego confirme con el botón 'Aceptar Cambios'.")
        End If
    End Sub

End Class
