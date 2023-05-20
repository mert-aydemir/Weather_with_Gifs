Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.IO

Public Class Form1
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        ' ÖNEMLİ NOT 1: BANA ÇIKAN PROJE, DÜNYADAKİ 3 BAŞKENTİ ALIP, HAVA DURUMLARINI GÖSTERMEKTİ. BEN BUNA EK
        ' OLARAK, GİF İLE HAVA DURUMLARINI GÖSTERMEYE ÇALIŞTIM.




        ' ÖNEMLİ NOT 2: HAREKETLİ GÖRSELLERİN "PATH"LARINI ALIRKEN MANUEL OLARAK GİRMEYİ ENGELLEMEK İÇİN, DOSYAYI
        ' PROGRAMIN DEBUG BÖLÜMÜNE ATARAK, PROGRAM YOLUNU ALIP, TAMAMEN OTOMATİK BİR ŞEKİLDE GİFLERE ERİŞİM SAĞLAMAYA
        ' ÇALIŞTIM. BAŞKA CİHAZLARDA TEST EDEMEDİĞİM İÇİN SORUNSUZ ÇALIŞIP ÇALIŞMAYACAĞINDAN EMİN DEĞİLİM.
        ' AMA ÇALIŞMASI GEREKTİĞİNİ DÜŞÜNÜYORUM.



        ' OpenWeather API'den gelen hava durumu açıklamaları İngilizce oldukları için böyle bir çözüm yolu denedim.

        Dim weatherDescriptions As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
    {"clear sky", "Açık"},
    {"few clouds", "Parçalı Bulutlu"},
    {"scattered clouds", "Parçalı Bulutlu"},
    {"broken clouds", "Bulutlu"},
    {"overcast clouds", "Bulutlu"},
    {"shower rain", "Sağanak Yağışlı"},
    {"rain", "Yağmurlu"},
    {"thunderstorm", "Gök Gürültülü Sağanak Yağışlı"},
    {"snow", "Kar Yağışlı"},
    {"mist", "Sisli"},
    {"light rain", "Hafif Yağmurlu"},
    {"snow depth", "Yoğun Kar Yağışlı"},
    {"light intensity shower rain", "Hafif Yağmurlu"},
    {"moderate rain", "Hafif Yağmurlu"},
    {"light snow", "Kar Yağışlı"},
    {"haze", "Hava Kirliliği Kaynaklı Sis"}
}



        ' API Anahtarımı, değişkene atayarak, kullanımını kolaylaştırıyorum.
        Dim API_KEY As String = "API KEY"

        ' Seçilen şehri ComboBox'tan alıyorm.
        Dim city As String = ComboBox1.SelectedItem.ToString()

        ' OpenWeatherMap API'den hava durumu verilerini çekmek için gereken URL'yi oluşturdum.
        Dim url As String = "http://api.openweathermap.org/data/2.5/weather?q=" & city & "&appid=" & API_KEY & "&units=metric"

        ' URL'yi kullanarak API'den hava durumu verilerini alıyorum.
        Dim request As HttpWebRequest = WebRequest.Create(url)
        Dim response As HttpWebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim json As String = reader.ReadToEnd()

        ' JSON verilerini işliyorum.
        Dim data As JObject = JObject.Parse(json)
        Dim temperature As Double = data("main")("temp")
        Dim humidity As Integer = data("main")("humidity")
        Dim weather As JArray = data("weather")
        Dim description As String = weather(0)("description")
        Dim icon As String = weather(0)("icon")
        Dim minTemperature As Double = data("main")("temp_min")
        Dim maxTemperature As Double = data("main")("temp_max")

        ' İngilizce hava durumu açıklamasını Türkçe'ye çeviriyorum.
        If weatherDescriptions.ContainsKey(description) Then
            description = weatherDescriptions(description)
        End If

        ' Label'ları güncelledim.
        Label4.Text = temperature.ToString() & " °C"
        Label5.Text = description
        Label6.Text = humidity.ToString() & "%"
        lbl_max_sicaklik.Text = maxTemperature.ToString() & " °C"
        lbl_min_sicaklik.Text = minTemperature.ToString() & " °C"

        Try
            If description = "Yağmurlu" Then
                Dim imagePath As String = Path.GetFullPath(Application.StartupPath & "\Weather Gifs\rain.gif")

                If IO.File.Exists(imagePath) Then 'Dosya mevcutsa PictureBox1 içine ekliyorum
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If

            ElseIf description = "Bulutlu" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\clouds.gif" 'Dosya yolunu oluşturuyorum
                If IO.File.Exists(imagePath) Then 'Dosya mevcutsa PictureBox1 içine ekliyorum
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If

            ElseIf description = "Kar Yağışlı" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\snow.gif"
                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If

            ElseIf description = "Sisli" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\foggy.gif"
                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If

            ElseIf description = "Açık" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\sun.gif"
                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If
            ElseIf description = "Parçalı Bulutlu" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\parcali_cloudy.gif"
                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If
            ElseIf description = "Yoğun Kar Yağışlı" Then
                Dim imagePath As String = Path.GetFullPath(Application.StartupPath & "\Weather Gifs\snowflake.gif")

                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If

            ElseIf description = "Hafif Yağmurlu" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\drizzle.gif"
                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If

            ElseIf description = "Sağanak Yağışlı" Then
                Dim imagePath As String = Application.StartupPath & "\Weather Gifs\storm.gif"
                If IO.File.Exists(imagePath) Then
                    PictureBox1.Image = Image.FromFile(imagePath)
                Else
                    MessageBox.Show("Dosya bulunamadı: " & imagePath)
                End If


            Else
                PictureBox1.Load("http://openweathermap.org/img/wn/" & icon & ".png")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        ' Bağlantıyı kapatıyorum.
        response.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

' MERT AYDEMİR - 222103509
