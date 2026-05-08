using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace suliradio
{
    public class ApiKeresek
    {
        private readonly string _baseUrl = "http://127.0.0.1:8000/api";
        private readonly string _token;

        public ApiKeresek(string token)
        {
            _token = token;
        }

        // ============ TESZT ============
        /// <summary>
        /// Backend működésének tesztelésére használható
        /// </summary>
        public async Task<bool> Teszt()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/teszttt");
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a tesztnél: {ex.Message}");
                return false;
            }
        }

        // ============ FELHASZNÁLÓ - NEMPUBLIKUS ============
        /// <summary>
        /// Token alapján lekéri az adott felhasználót
        /// </summary>
        public async Task<JsonElement?> FelhasznaloBezerzesTokenbol(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    var response = await client.GetAsync($"{_baseUrl}/felhasznalotokenbol/{token}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<JsonElement>(content);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a felhasználó lekérésekor: {ex.Message}");
                return null;
            }
        }

        // ============ IDŐ ============
        /// <summary>
        /// Visszaadja az aktuális budapesti időt
        /// </summary>
        public async Task<string> Ido()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/ido");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var ido = JsonSerializer.Deserialize<JsonElement>(content);
                        return ido.GetString() ?? string.Empty;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az idő lekérésekor: {ex.Message}");
                return null;
            }
        }

        // ============ BEJELENTKEZÉS & REGISZTRÁCIÓ - PUBLIKUS ============
        /// <summary>
        /// Normális felhasználó regisztrációja (nem szükséges token)
        /// </summary>
        public async Task<bool> Regisztracio(string email, string jelszoBaze64, string keresztnev, string vezeteknev, string omazonosito = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new
                    {
                        email = email,
                        jelszo = jelszoBaze64,
                        keresztnev = keresztnev,
                        vezeteknev = vezeteknev,
                        omazonosito = omazonosito
                    };

                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUrl}/regisztracio", content);
                    return response.StatusCode == System.Net.HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a regisztrációnál: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Nem normális felhasználó regisztrációja (nempublikus - token szükséges)
        /// </summary>
        public async Task<bool> NemNormalFelhasznaloRegisztracio(string email, string jelszoBaze64, string keresztnev, string vezeteknev, int jog = 1, string omazonosito = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new
                    {
                        email = email,
                        jelszo = jelszoBaze64,
                        keresztnev = keresztnev,
                        vezeteknev = vezeteknev,
                        jog = jog,
                        omazonosito = omazonosito
                    };

                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUrl}/nemnormalfelhasznaloregisztracio", content);
                    return response.StatusCode == System.Net.HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a nem normális felhasználó regisztrációnál: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Felhasználó bejelentkezése (nem szükséges token)
        /// </summary>
        public async Task<string> Bejelentkezes(string email, string jelszoBaze64)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new
                    {
                        email = email,
                        jelszo = jelszoBaze64
                    };

                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUrl}/bejelentkezes", content);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        // Try to parse various possible response formats:
                        // 1) raw string token
                        // 2) JSON string
                        // 3) JSON object with { "token": "..." } or { "access_token": "..." }
                        try
                        {
                            var je = JsonSerializer.Deserialize<JsonElement>(responseContent);
                            if (je.ValueKind == JsonValueKind.String)
                            {
                                return je.GetString() ?? string.Empty;
                            }
                            if (je.ValueKind == JsonValueKind.Object)
                            {
                                if (je.TryGetProperty("token", out var t) && t.ValueKind == JsonValueKind.String)
                                    return t.GetString() ?? string.Empty;
                                if (je.TryGetProperty("access_token", out var at) && at.ValueKind == JsonValueKind.String)
                                    return at.GetString() ?? string.Empty;
                                if (je.TryGetProperty("Token", out var t2) && t2.ValueKind == JsonValueKind.String)
                                    return t2.GetString() ?? string.Empty;
                            }

                            // fallback: return trimmed raw content
                            return responseContent.Trim('"');
                        }
                        catch
                        {
                            // not JSON, return raw
                            return responseContent.Trim('"');
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a bejelentkezésnél: {ex.Message}");
                return null;
            }
        }

        // ============ BEJELENTKEZÉS/KIJELENTKEZÉS ============
        /// <summary>
        /// Felhasználó kijelentkezése
        /// </summary>
        public async Task<bool> Kilepes()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/kilepes");
                    return response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a kilépésnél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Felhasználó kijelentkeztetése email alapján (nempublikus - legalább 3-as jog szükséges)
        /// </summary>
        public async Task<bool> KileptetesEmailAlapjan(string email)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/kileptetesemailalapjan/{email}");
                    return response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a kijelentkeztetésnél: {ex.Message}");
                return false;
            }
        }

        // ============ FELHASZNÁLÓ TÖRLÉSE ============
        /// <summary>
        /// Felhasználó törlése email alapján (nempublikus - admin jog szükséges)
        /// </summary>
        public async Task<bool> FelhasznaloTorlesesEmailAlapjan(string email)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.DeleteAsync($"{_baseUrl}/felhasznalotorlese/{email}");
                    return response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a felhasználó törlésekor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Saját felhasználó törlése
        /// </summary>
        public async Task<bool> SajatFelhasznaloTorlese()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.DeleteAsync($"{_baseUrl}/felhasznalotorlese");
                    return response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a saját felhasználó törlésekor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Aktív felhasználók száma
        /// </summary>
        public async Task<int> AktivFelhasznaloSzam()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/aktivfelhasznaloszam");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var json = JsonSerializer.Deserialize<JsonElement>(content);
                        if (json.TryGetProperty("count(id)", out var countElement))
                        {
                            return countElement.GetInt32();
                        }
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az aktív felhasználó szám lekérésekor: {ex.Message}");
                return -1;
            }
        }

        // ============ ZENE - LEJÁTSZÁS ============
        /// <summary>
        /// Jelenleg játszani kívánt zene útvonala
        /// </summary>
        public async Task<string> LejatszandoZene()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/lejatszandozene");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<JsonElement>(content).GetString() ?? string.Empty;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a lejátszandó zene lekérésekor: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Jelenleg játszani kívánt zene összes adatával
        /// </summary>
        public async Task<JsonElement?> LejatszandoZeneMindenadat()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/lejatszandozenemindenadat");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<JsonElement>(content);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a lejátszandó zene adatainak lekérésekor: {ex.Message}");
                return null;
            }
        }

        // ============ ZENE - KEZELÉS ============
        /// <summary>
        /// Az összes zene lekérése
        /// </summary>
        public async Task<List<Zenek>> OsszesZene()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/osszeszene");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
                        return ParseZeneList(jsonElement);
                    }
                    return new List<Zenek>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az összes zene lekérésekor: {ex.Message}");
                return new List<Zenek>();
            }
        }

        /// <summary>
        /// Zene törlése ID alapján (nempublikus)
        /// </summary>
        public async Task<bool> ZeneTorlesIdAlapjan(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.DeleteAsync($"{_baseUrl}/zenetorlesidalapjan/{id}");
                    return response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene törlésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Zene frissítése (nempublikus)
        /// </summary>
        public async Task<bool> ZeneFreissites(Dictionary<string, object> zeneAdatok)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var json = JsonSerializer.Serialize(zeneAdatok);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{_baseUrl}/zenefrissites", content);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene frissítésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Zene útvonalának frissítése URL alapján (nempublikus)
        /// </summary>
        public async Task<bool> ZeneUtvonalFrissitesUrlAlapjan(string url, string ujUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new { zeneurl = ujUrl };
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{_baseUrl}/zeneutvonalfrissitesurlalpjan/{Uri.EscapeDataString(url)}", content);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene útvonalának frissítésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Zene útvonalának frissítése ID alapján (nempublikus)
        /// </summary>
        public async Task<bool> ZeneUtvonalFrissitesIdAlapjan(int id, string ujUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new { zeneurl = ujUrl };
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{_baseUrl}/zeneutvonalfrissitesidalapjan/{id}", content);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene útvonalának frissítésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Zene törlése (nempublikus)
        /// </summary>
        public async Task<bool> ZeneTorles(Dictionary<string, object> zeneAdat)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var json = JsonSerializer.Serialize(zeneAdat);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/zenetorles")
                    {
                        Content = content
                    };

                    var response = await client.SendAsync(request);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene törlésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Zene feltöltése
        /// </summary>
        public async Task<bool> ZeneFeltoltes(Dictionary<string, object> zeneAdatok)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var json = JsonSerializer.Serialize(zeneAdatok);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{_baseUrl}/zenefeltoltes", content);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene feltöltésekor: {ex.Message}");
                return false;
            }
        }

        // ============ LEJÁTSZÁSI LISTA ============
        /// <summary>
        /// Lejátszás törlése (nempublikus)
        /// </summary>
        public async Task<bool> LejatszasTorles(int lejatszasId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new { id = lejatszasId };
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/lejatszastorles")
                    {
                        Content = content
                    };

                    var response = await client.SendAsync(httpRequest);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a lejátszás törlésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Mai napi órarend - visszaadja az összes órarend-bejegyzést tuple listaként
        /// </summary>
        public async Task<List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)>> MainapiOrarend()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/mainapiorarend");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);

                        var result = new List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)>();

                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in jsonElement.EnumerateArray())
                            {
                                int id = 0;
                                int zeneId = 0;
                                TimeSpan mettol = TimeSpan.Zero;
                                TimeSpan meddig = TimeSpan.Zero;

                                if (item.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.Number)
                                    id = idProp.GetInt32();
                                if (item.TryGetProperty("zeneid", out var zeneIdProp) && zeneIdProp.ValueKind == JsonValueKind.Number)
                                    zeneId = zeneIdProp.GetInt32();

                                if (item.TryGetProperty("mikortol", out var mikortolProp))
                                {
                                    var s = mikortolProp.GetString();
                                    if (!string.IsNullOrEmpty(s))
                                    {
                                        if (DateTime.TryParse(s, out var dt))
                                            mettol = dt.TimeOfDay;
                                        else if (TimeSpan.TryParse(s, out var ts))
                                            mettol = ts;
                                    }
                                }

                                if (item.TryGetProperty("meddig", out var meddigProp))
                                {
                                    var s = meddigProp.GetString();
                                    if (!string.IsNullOrEmpty(s))
                                    {
                                        if (DateTime.TryParse(s, out var dt2))
                                            meddig = dt2.TimeOfDay;
                                        else if (TimeSpan.TryParse(s, out var ts2))
                                            meddig = ts2;
                                    }
                                }

                                result.Add((id, zeneId, mettol, meddig));
                            }
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.Object)
                        {
                            int id = 0;
                            int zeneId = 0;
                            TimeSpan mettol = TimeSpan.Zero;
                            TimeSpan meddig = TimeSpan.Zero;

                            if (jsonElement.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.Number)
                                id = idProp.GetInt32();
                            if (jsonElement.TryGetProperty("zeneid", out var zeneIdProp) && zeneIdProp.ValueKind == JsonValueKind.Number)
                                zeneId = zeneIdProp.GetInt32();

                            if (jsonElement.TryGetProperty("mikortol", out var mikortolProp))
                            {
                                var s = mikortolProp.GetString();
                                if (!string.IsNullOrEmpty(s))
                                {
                                    if (DateTime.TryParse(s, out var dt))
                                        mettol = dt.TimeOfDay;
                                    else if (TimeSpan.TryParse(s, out var ts))
                                        mettol = ts;
                                }
                            }

                            if (jsonElement.TryGetProperty("meddig", out var meddigProp))
                            {
                                var s = meddigProp.GetString();
                                if (!string.IsNullOrEmpty(s))
                                {
                                    if (DateTime.TryParse(s, out var dt2))
                                        meddig = dt2.TimeOfDay;
                                    else if (TimeSpan.TryParse(s, out var ts2))
                                        meddig = ts2;
                                }
                            }

                            result.Add((id, zeneId, mettol, meddig));
                        }

                        return result;
                    }
                    return new List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a mai napi órarend lekérésekor: {ex.Message}");
                return new List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)>();
            }
        }

        /// <summary>
        /// Teljes órarend
        /// </summary>
        public async Task<JsonElement?> TeljesOrarend()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/teljesorarend");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<JsonElement>(content);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a teljes órarend lekérésekor: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Következő lejátszás
        /// </summary>
        public async Task<JsonElement?> KovetkezoLejatszas()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/kovetkezolejatszas");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<JsonElement>(content);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a következő lejátszás lekérésekor: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Zene validációja ID alapján
        /// </summary>
        public async Task<JsonElement?> ZeneValidacio(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.PostAsync($"{_baseUrl}/zenevalidacio/{id}",null);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<JsonElement>(content);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene validációjánál: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Várólistában lévő zenék
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> VaroLista()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/varolista");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
                        
                        var result = new List<KeyValuePair<int, string>>();
                        
                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in jsonElement.EnumerateArray())
                            {
                                int id = 0;
                                string url = string.Empty;
                                
                                if (item.TryGetProperty("id", out var idProp))
                                    id = idProp.GetInt32();
                                if (item.TryGetProperty("keresurl", out var urlProp))
                                    url = urlProp.GetString() ?? string.Empty;
                                
                                result.Add(new KeyValuePair<int, string>(id, url));
                            }
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.Object)
                        {
                            // fallback egyetlen objektumra
                            int id = 0;
                            string url = string.Empty;
                            
                            if (jsonElement.TryGetProperty("id", out var idProp))
                                id = idProp.GetInt32();
                            if (jsonElement.TryGetProperty("keresurl", out var urlProp))
                                url = urlProp.GetString() ?? string.Empty;
                            

                            result.Add(new KeyValuePair<int, string>(id, url));
                        }
                        
                        return result;
                    }
                    return new List<KeyValuePair<int, string>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a várólistának a lekérésekor: {ex.Message}");
                return new List<KeyValuePair<int, string>>();
            }
        }

        /// <summary>
        /// Lejátszható zenék
        /// </summary>
        public async Task<List<Zenek>> LejatszhatoZenek()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/lejatszhatozenek");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
                        return ParseZeneList(jsonElement);
                    }
                    return new List<Zenek>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a lejátszható zenék lekérésekor: {ex.Message}");
                return new List<Zenek>();
            }
        }

        // ============ ÓRAREND ============
        /// <summary>
        /// Órarend manuális hozzáadása
        /// </summary>
        public async Task<bool> OrarendManualishozzaadas(int zeneId, long mikortol, long meddig)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new
                    {
                        zeneid = zeneId,
                        mikortol = mikortol,
                        meddig = meddig
                    };

                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUrl}/orarendmanualishozzaadas", content);
                    return response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az órarend hozzáadásánál: {ex.Message}");
                return false;
            }
        }

        // ============ SZÜNETEK ============
        /// <summary>
        /// Szünetek listája
        /// </summary>
        public async Task<List<CsengetesiRend>> SzunetekListaja()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/szuneteklistaja");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
                        
                        var szunetList = new List<CsengetesiRend>();
                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in jsonElement.EnumerateArray())
                            {
                                int hanyadik = 0;
                                TimeSpan kezdes = TimeSpan.Zero;
                                TimeSpan vege = TimeSpan.Zero;
                                
                                if (item.TryGetProperty("hanyadik", out var hanyadikProp))
                                    hanyadik = hanyadikProp.GetInt32();
                                if (item.TryGetProperty("kezdes", out var kezdesProp))
                                    kezdes = ParseTimeFromDateTimeString(kezdesProp.GetString());
                                if (item.TryGetProperty("vege", out var vegeProp))
                                    vege = ParseTimeFromDateTimeString(vegeProp.GetString());
                                
                                szunetList.Add(new CsengetesiRend(hanyadik, kezdes, vege));
                            }
                        }
                        return szunetList;
                    }
                    return new List<CsengetesiRend>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a szünetek listájának lekérésekor: {ex.Message}");
                return new List<CsengetesiRend>();
            }
        }

        /// <summary>
        /// Szünet törlése
        /// </summary>
        public async Task<bool> SzunetTorles(int hanyadik)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.DeleteAsync($"{_baseUrl}/szunettorles/{hanyadik}");
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a szünet törlésénél: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Szünet hozzáadása
        /// </summary>
        public async Task<bool> SzunetHozzaadas(int hanyadik, TimeSpan kezdes, TimeSpan vege)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new
                    {
                        hanyadik = hanyadik,
                        kezdes = FormatTimeForBackend(kezdes),
                        vege = FormatTimeForBackend(vege)
                    };

                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUrl}/szunethozzaadas", content);
                    return response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a szünet hozzáadásánál: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Szünet módosítása
        /// </summary>
        public async Task<bool> SzunetModositas(int hanyadik, TimeSpan? kezdes = null, TimeSpan? vege = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);

                    var request = new Dictionary<string, string>();
                    if (kezdes.HasValue)
                        request["kezdes"] = FormatTimeForBackend(kezdes.Value);
                    if (vege.HasValue)
                        request["vege"] = FormatTimeForBackend(vege.Value);

                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{_baseUrl}/szunetmodositas/{hanyadik}", content);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a szünet módosításánál: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Zene kérése
        /// </summary>
        public async Task<bool> ZeneKeres(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    url = Uri.UnescapeDataString(url);
                    var request = new { zeneurl = url };
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUrl}/bekeres", content);
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene kérésekor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Összes zene kérés listázása (nempublikus - legalább tanár jog szükséges)
        /// </summary>
        public async Task<List<Zenek>> KeresesListazasa()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _token);
                    var response = await client.GetAsync($"{_baseUrl}/kereseklistazasa");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);

                        var zeneList = new List<Zenek>();

                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in jsonElement.EnumerateArray())
                            {
                                var zene = new Zenek();
                                
                                if (item.TryGetProperty("id", out var id))
                                    zene.ZeneID = id.GetInt32();
                                if (item.TryGetProperty("zeneurl", out var url))
                                    zene.EleresiUt = url.GetString() ?? "";
                                if (item.TryGetProperty("felhasznaloid", out var felhasznaloId))
                                    zene.FajlNev = felhasznaloId.GetInt32().ToString(); // felhasznaloid-t a FajlNev mezőbe tesszük
                                if (item.TryGetProperty("validalte", out var validalte))
                                    zene.Cim = validalte.GetInt32().ToString(); // validalte értéket a Cim mezőbe tesszük
                                if (item.TryGetProperty("mikor", out var mikor))
                                {
                                    var mikortartalom = mikor.GetString();
                                    zene.Eloado = mikortartalom ?? ""; // mikor időt az Eloado mezőbe tesszük
                                }

                                zeneList.Add(zene);
                            }
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.Object)
                        {
                            // Fallback egyetlen objektumra
                            var zene = new Zenek();
                            
                            if (jsonElement.TryGetProperty("id", out var id))
                                zene.ZeneID = id.GetInt32();
                            if (jsonElement.TryGetProperty("zeneurl", out var url))
                                zene.EleresiUt = url.GetString() ?? "";
                            if (jsonElement.TryGetProperty("felhasznaloid", out var felhasznaloId))
                                zene.FajlNev = felhasznaloId.GetInt32().ToString();
                            if (jsonElement.TryGetProperty("validalte", out var validalte))
                                zene.Cim = validalte.GetInt32().ToString();
                            if (jsonElement.TryGetProperty("mikor", out var mikor))
                            {
                                var mikortartalom = mikor.GetString();
                                zene.Eloado = mikortartalom ?? "";
                            }

                            zeneList.Add(zene);
                        }

                        return zeneList;
                    }
                    return new List<Zenek>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a kérések listázásakor: {ex.Message}");
                return new List<Zenek>();
            }
        }

        /// <summary>
        /// Zenék listájának feldolgozása JsonElement-ből és Zenek objektumokra konvertálása
        /// </summary>
        private List<Zenek> ParseZeneList(JsonElement jsonElement)
        {
            var zeneList = new List<Zenek>();
            try
            {
                if (jsonElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in jsonElement.EnumerateArray())
                    {
                        var zene = new Zenek();
                        if (item.TryGetProperty("id", out var id))
                            zene.ZeneID = id.GetInt32();
                        if (item.TryGetProperty("zeneurl", out var url))
                            zene.EleresiUt = url.GetString() ?? "";
                        if (item.TryGetProperty("eloado", out var eloado))
                            zene.Eloado = eloado.GetString() ?? "";
                        if (item.TryGetProperty("cim", out var cim))
                            zene.Cim = cim.GetString() ?? "";
                        if (item.TryGetProperty("fajlnev", out var fajlnev))
                            zene.FajlNev = fajlnev.GetString() ?? "";
                        if (item.TryGetProperty("hossz", out var hossz))
                        {
                            if (hossz.ValueKind == JsonValueKind.Number)
                            {
                                // backend may return seconds as number
                                zene.Hossz = TimeSpan.FromSeconds(hossz.GetDouble());
                            }
                            else if (hossz.ValueKind == JsonValueKind.String && TimeSpan.TryParse(hossz.GetString(), out var ts))
                            {
                                zene.Hossz = ts;
                            }
                        }
                        if (item.TryGetProperty("tema", out var tema))
                            zene.ZeneForrasa = Zenek.Forras.Mappa; // alapértelmezett

                        zeneList.Add(zene);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zeneglek feldolgozásakor: {ex.Message}");
            }
            return zeneList;
        }

        /// <summary>
        /// Időt extrahál egy dátum-idő stringből (pl. "2026-05-06 09:00:00" -> 09:00:00)
        /// </summary>
        private TimeSpan ParseTimeFromDateTimeString(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
                return TimeSpan.Zero;

            // Próbáljuk meg DateTime-ként parseolni
            if (DateTime.TryParse(dateTimeString, out var dateTime))
            {
                return dateTime.TimeOfDay;
            }

            // Fallback: próbáljuk meg közvetlenül TimeSpan-ként parseolni
            if (TimeSpan.TryParse(dateTimeString, out var timeSpan))
            {
                return timeSpan;
            }

            Console.WriteLine($"Figyelmeztetés: Nem sikerült parseolni az időt: {dateTimeString}");
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Időt konvertál a backend által várt formátumra (yyyy-MM-dd HH:mm:ss)
        /// Jelenleg csak az idő részt tartalmazza, de a backend dátumot is vár
        /// </summary>
        private string FormatTimeForBackend(TimeSpan timeSpan)
        {
            // Ma dátumához hozzáadjuk az időt a backend formátumában
            var now = DateTime.Now;
            var dateTime = now.Date.Add(timeSpan);
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Zene adatai ID alapján - visszaadja az adott ID-hez tartozó zene rekordját
        /// </summary>
        public async Task<Zenek> ZeneAdataiIdAlapjan(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/idalapjankapottzeneadatai/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);

                        var zene = new Zenek();
                        
                        if (jsonElement.TryGetProperty("id", out var idProp))
                            zene.ZeneID = idProp.GetInt32();
                        if (jsonElement.TryGetProperty("zeneurl", out var urlProp))
                            zene.EleresiUt = urlProp.GetString() ?? "";
                        if (jsonElement.TryGetProperty("eloado", out var eloado))
                            zene.Eloado = eloado.GetString() ?? "";
                        if (jsonElement.TryGetProperty("cim", out var cim))
                            zene.Cim = cim.GetString() ?? "";
                        if (jsonElement.TryGetProperty("hossz", out var hossz))
                        {
                            if (hossz.ValueKind == JsonValueKind.Number)
                            {   
                                zene.Hossz = TimeSpan.FromSeconds(hossz.GetDouble());
                            }
                            else if (hossz.ValueKind == JsonValueKind.String && TimeSpan.TryParse(hossz.GetString(), out var ts))
                            {
                                zene.Hossz = ts;
                            }
                        }
                        if (jsonElement.TryGetProperty("tema", out var tema))
                            zene.ZeneForrasa = Zenek.Forras.Mappa;
                        if (jsonElement.TryGetProperty("keresurl", out var keresurl))
                            zene.FajlNev = keresurl.GetString() ?? "";

                        return zene;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene adatainak lekérésekor ID alapján: {ex.Message}");
                return null;
            }
        }
    }
}
