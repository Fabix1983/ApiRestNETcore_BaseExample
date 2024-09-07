using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Text;
using System.IO;
using ApiRestNETcore_BaseExample.Models;
using System.ComponentModel;

namespace ApiRestNETcore_BaseExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Comuni : ControllerBase
    {
        public string fileName = Path.GetFullPath(Directory.GetCurrentDirectory() + "/Data/ComuniItaliani.csv");

        // GET: api/<Comuni>/GetAll
        [HttpGet("[action]")]
        public ActionResult<ListaComuni> GetAll()
        {
            ListaComuni ComuniOUT = new ListaComuni();

            try
            {         
                DataTable ListaComuniFromFile = ConvertCSVtoDataTable(fileName, ';');

                if (ListaComuniFromFile.Rows.Count > 0)
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = null;

                    ComuniOUT.Comuni = new List<Comune>();

                    foreach (DataRow row in ListaComuniFromFile.Rows)
                    {
                        Comune Comune = new Comune();
                        {
                            Comune.comune = row["comune"].ToString().Trim();
                            Comune.regione = row["regione"].ToString().Trim();
                            Comune.provincia = row["provincia"].ToString().Trim();
                            Comune.prefisso = row["prefisso"].ToString().Trim();
                            Comune.numeroresidenti = Convert.ToInt32(row["numeroresidenti"]);
                        };
                        ComuniOUT.Comuni.Add(Comune);
                    }
                }
            }
            catch (Exception ex)
            {
                ComuniOUT.response_status = "KO";
                ComuniOUT.response_error = ex.ToString();
            }

            return ComuniOUT;
        }

        // GET: api/<Comuni>/GetComuniPerProvincia
        [HttpGet("[action]/{provincia}")]
        public ActionResult<ListaComuni> GetComuniPerProvincia(String provincia)
        {
            ListaComuni ComuniOUT = new ListaComuni();

            try
            {
                DataTable ListaComuniFromFile = ConvertCSVtoDataTable(fileName, ';');

                var result = ListaComuniFromFile.AsEnumerable().Where(myRow => myRow.Field<string>("provincia").ToUpper() == provincia.ToUpper()).OrderBy(x => x.Field<string>("comune"));

                DataTable ListaComuniFromProvincia = new DataTable();

                if (result != null)
                {
                    ListaComuniFromProvincia = result.CopyToDataTable();

                    if (ListaComuniFromProvincia.Rows.Count > 0)
                    {
                        ComuniOUT.response_status = "OK";
                        ComuniOUT.response_error = null;

                        ComuniOUT.Comuni = new List<Comune>();

                        foreach (DataRow row in ListaComuniFromProvincia.Rows)
                        {
                            Comune Comune = new Comune();
                            {
                                Comune.comune = row["comune"].ToString().Trim();
                                Comune.regione = row["regione"].ToString().Trim();
                                Comune.provincia = row["provincia"].ToString().Trim();
                                Comune.prefisso = row["prefisso"].ToString().Trim();
                                Comune.numeroresidenti = Convert.ToInt32(row["numeroresidenti"]);
                            };
                            ComuniOUT.Comuni.Add(Comune);
                        }
                    }
                    else
                    {
                        ComuniOUT.response_status = "OK";
                        ComuniOUT.response_error = "No Record Found";
                    }
                }
                else
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The source contains no DataRows"))
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = "No Record Found";
                }
                else
                {
                    ComuniOUT.response_status = "KO";
                    ComuniOUT.response_error = "ERROR: " + ex.ToString();
                }
            }

            return ComuniOUT;
        }

        // GET: api/<Comuni>/GetComuniPerRegione
        [HttpGet("[action]/{regione}")]
        public ActionResult<ListaComuni> GetComuniPerRegione(String regione)
        {
            ListaComuni ComuniOUT = new ListaComuni();

            try
            {
                DataTable ListaComuniFromFile = ConvertCSVtoDataTable(fileName, ';');

                var result = ListaComuniFromFile.AsEnumerable().Where(myRow => myRow.Field<string>("regione").ToUpper() == regione.ToUpper()).OrderBy(x => x.Field<string>("comune"));

                DataTable ListaComuniFromRegione = new DataTable();
          
                if (result != null)
                {
                    ListaComuniFromRegione = result.CopyToDataTable();

                    if (ListaComuniFromRegione.Rows.Count > 0)
                    {
                        ComuniOUT.response_status = "OK";
                        ComuniOUT.response_error = null;

                        ComuniOUT.Comuni = new List<Comune>();

                        foreach (DataRow row in ListaComuniFromRegione.Rows)
                        {
                            Comune Comune = new Comune();
                            {
                                Comune.comune = row["comune"].ToString().Trim();
                                Comune.regione = row["regione"].ToString().Trim();
                                Comune.provincia = row["provincia"].ToString().Trim();
                                Comune.prefisso = row["prefisso"].ToString().Trim();
                                Comune.numeroresidenti = Convert.ToInt32(row["numeroresidenti"]);
                            };
                            ComuniOUT.Comuni.Add(Comune);
                        }
                    }
                    else
                    {
                        ComuniOUT.response_status = "OK";
                        ComuniOUT.response_error = "No Record Found";
                    }
                }
                else
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The source contains no DataRows"))
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = "No Record Found";
                }
                else
                {
                    ComuniOUT.response_status = "KO";
                    ComuniOUT.response_error = "ERROR: " + ex.ToString();
                }
            }

            return ComuniOUT;
        }

        // GET: api/<Comuni>/GetComuniPerResidenti
        [HttpGet("[action]/{residenti}")]
        public ActionResult<ListaComuni> GetComuniPerResidenti(int residenti)
        {
            ListaComuni ComuniOUT = new ListaComuni();

            try
            {
                DataTable ListaComuniFromFile = ConvertCSVtoDataTable(fileName, ';');

                if (ListaComuniFromFile.Rows.Count > 0)
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = null;

                    ComuniOUT.Comuni = new List<Comune>();

                    foreach (DataRow row in ListaComuniFromFile.Rows)
                    {
                        if (Convert.ToInt32(row["numeroresidenti"]) >= residenti)
                        {
                            Comune Comune = new Comune();
                            {
                                Comune.comune = row["comune"].ToString().Trim();
                                Comune.regione = row["regione"].ToString().Trim();
                                Comune.provincia = row["provincia"].ToString().Trim();
                                Comune.prefisso = row["prefisso"].ToString().Trim();
                                Comune.numeroresidenti = Convert.ToInt32(row["numeroresidenti"]);
                            };
                            ComuniOUT.Comuni.Add(Comune);
                        }
                    }
                }
                else
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The source contains no DataRows"))
                {
                    ComuniOUT.response_status = "OK";
                    ComuniOUT.response_error = "No Record Found";
                }
                else
                {
                    ComuniOUT.response_status = "KO";
                    ComuniOUT.response_error = "ERROR: " + ex.ToString();
                }
            }

            return ComuniOUT;
        }

        private static DataTable ConvertCSVtoDataTable(string strFilePath, char ColumnSeparatorChart)
        {
            DataTable dt = new DataTable();

            // Uso Encoding.Default per la codifica dei caratteri speciali

            using (StreamReader sr = new StreamReader(strFilePath, Encoding.Default))
            {
                string[] headers = sr.ReadLine().Split(ColumnSeparatorChart);
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(ColumnSeparatorChart);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }

}
