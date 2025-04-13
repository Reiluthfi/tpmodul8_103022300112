using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    private const string FileName = "Covid_Config.json";

    public string satuan_suhu { get; set; }
    public int batas_hari_deman { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    public CovidConfig()
    {
        SetDefault();
    }

    public static CovidConfig Load()
    {
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<CovidConfig>(json);
        }
        else
        {
            var config = new CovidConfig();
            config.SaveConfig();
            return config;
        }
    }

    private void SetDefault()
    {
        satuan_suhu = "celcius";
        batas_hari_deman = 14;
        pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
    }

    public void SaveConfig()
    {
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);
    }

    public void UbahSatuan()
    {
        satuan_suhu = satuan_suhu == "celcius" ? "fahrenheit" : "celcius";
        SaveConfig();
    }
}
