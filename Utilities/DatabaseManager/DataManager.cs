using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace MundoRP
{
	public class DataManager
	{
        const string server = "127.0.0.1";
        const string port = "3306";
        const string user = "root";
        const string password = "mundorpadmin1";

        MySqlConnection sqlConn = new MySqlConnection("server=" + server + ";port=" + port + ";User Id=" + user + ";password=" + password);

        public string openDB()
        {
            try
            {

                sqlConn.Open();
                sqlConn.Close();
                return "MundoRP | Banco de Dados aberto com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public void truncateTable(string table)
        {
            try
            {
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("TRUNCATE mundorp."+table, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return;
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
            }
        }
        public int count(string table)
        {
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT COUNT(*) FROM mundorp." + table, sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                dr.Read();
                int tableReturn = Convert.ToInt32(dr.GetString(0));
                sqlConn.Close();
                return tableReturn;
            }
            catch
            {
                return 0;
            }
        }

        //GETTERS====================================================================================================================

        public MundoPlayer getPlayerBySteamId(Steamworks.CSteamID id)
        {
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT username, steamid, level, xp, job, premium, mp, rp FROM mundorp.players WHERE steamid=" + id.ToString(), sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                dr.Read();

                bool premium = false;
                premium = Convert.ToDateTime(dr.GetString(5)) > DateTime.Now ? true : false;
                List<GarageVehicle> garageVehicles = getVehiclesBySteamId(id);
                MundoPlayer player = new MundoPlayer(dr.GetString(0), id, Convert.ToInt32(dr.GetString(2)), Convert.ToInt32(dr.GetString(3)), dr.GetString(4), premium, (float)Convert.ToDouble(dr.GetString(7)), (float)Convert.ToDouble(dr.GetString(6)), garageVehicles);
                sqlConn.Close();
                
                return player;

            }
            catch(Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return null;
            }
        }

        public List<GarageVehicle> getVehiclesBySteamId(Steamworks.CSteamID id)
		{
            List<GarageVehicle> playerVehicles = new List<GarageVehicle>();
            try
            {

                sqlConn.Open();

                MySqlCommand sqlCmd = new MySqlCommand("SELECT Vehicle_uId, Vehicle_Battery, Vehicle_Health, Vehicle_Fuel, Vehicle_Name, Vehicle_Color, Vehicle_Id" +
                    "FROM mundorp.players" +
                    "INNER JOIN mundorp.vehicles" +
                    "INNER JOIN mundorp.vehiclesdata" +
                    "ON mundorp.vehicles.Vehicle_uniqueId = mundorp.vehiclesdata.Vehicle_uId" +
                    "WHERE mundorp.vehicles.Vehicle_OwnerId ="+ id.ToString() +
                    "AND mundorp.players.steamid =" + id.ToString(), sqlConn);

                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                int i = 0;
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int TableId = Convert.ToUInt16(dr.GetString(5));
                        string owner = id.ToString();
                        UInt16 vId = Convert.ToUInt16(dr.GetString(0));
                        string vColor= dr.GetString(4);
                        DateTime date = DateTime.Now;
                        ushort bat = Convert.ToUInt16(dr.GetString(1));
                        ushort hp = Convert.ToUInt16(dr.GetString(2));
                        ushort gas = Convert.ToUInt16(dr.GetString(3));
                        string vname = dr.GetString(4);
                        playerVehicles.Add(new GarageVehicle(TableId, owner, vId, vColor, date, bat, hp, gas, vname));
                        Rocket.Core.Logging.Logger.Log("Adicionado veículo: " + playerVehicles[i].vname);
                    }
                    dr.NextResult();    
                    i++;
                }
                sqlConn.Close();
                return playerVehicles;
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return null;
            }
        }

        public List<LataDeLixo> getGarbagesFromDB()
		{
            List<LataDeLixo> garbagesList = new List<LataDeLixo>();
            garbagesList.Clear();
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT posx, posy, posz FROM mundorp.garbages", sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LataDeLixo newLixo = new LataDeLixo((float) Convert.ToDouble(dr.GetString(0)), (float) Convert.ToDouble(dr.GetString(1)), (float) Convert.ToDouble(dr.GetString(2)));
                        garbagesList.Add(newLixo);
                    }
                    dr.NextResult();
                }
                sqlConn.Close();
                return garbagesList;
            }
            catch
            {
                return null;
            }
        }    
        
        public List<Aterro> getDumpsFromDB()
		{
            List<Aterro> dumpList = new List<Aterro>();
            dumpList.Clear();
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT posx, posy, posz FROM mundorp.dumps", sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Aterro newDump = new Aterro((float) Convert.ToDouble(dr.GetString(0)), (float) Convert.ToDouble(dr.GetString(1)), (float) Convert.ToDouble(dr.GetString(2)));
                        dumpList.Add(newDump);
                    }
                    dr.NextResult();
                }
                sqlConn.Close();
                return dumpList;
            }
            catch
            {
                return null;
            }
        }

        public List<Poste> getPostsFromDB()
        {
            List<Poste> postList = new List<Poste>();
            postList.Clear();
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT name, posx, posy, posz FROM mundorp.posts", sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
				{
					while (dr.Read())
					{
                        Poste newPoste = new Poste((float) Convert.ToDouble(dr.GetString(1)), (float) Convert.ToDouble(dr.GetString(2)), (float)Convert.ToDouble(dr.GetString(3)), dr.GetString(0));
                        postList.Add(newPoste);
					}
					dr.NextResult();
				}               
                sqlConn.Close();
                return postList;
            }
            catch
            {
                return null;
            }
        }

        public List<CaixaCorreio> getMailBoxesFromDB()
        {
            List<CaixaCorreio> mailboxList = new List<CaixaCorreio>();
            mailboxList.Clear();
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT name, posx, posy, posz FROM mundorp.posts", sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CaixaCorreio newMailbox = new CaixaCorreio((float)Convert.ToDouble(dr.GetString(1)), (float)Convert.ToDouble(dr.GetString(2)), (float)Convert.ToDouble(dr.GetString(3)), dr.GetString(0));
                        mailboxList.Add(newMailbox);
                    }
                    dr.NextResult();
                }
                sqlConn.Close();
                return mailboxList;
            }
            catch
            {
                return null;
            }
        }   
        
        public List<PontoOnibus> getBusstopsFromDB(string type)
        {
            List<PontoOnibus> busstopList = new List<PontoOnibus>();
            busstopList.Clear();
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT name, type, posx, posy, posz FROM mundorp.posts WHERE tipo="+type, sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PontoOnibus newBusstop = new PontoOnibus(dr.GetString(0), (float)Convert.ToDouble(dr.GetString(2)), (float)Convert.ToDouble(dr.GetString(3)), (float)Convert.ToDouble(dr.GetString(4)));
                        busstopList.Add(newBusstop);
                    }
                    dr.NextResult();
                }
                sqlConn.Close();
                return busstopList;
            }
            catch
            {
                return null;
            }
        }

        public string getById(string table, string column, int id)
        {
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT " + column + " FROM " + "mundorp" + "." + table + " WHERE id =" + id.ToString(), sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                dr.Read();
                string tableReturn = dr.GetString(0);
                sqlConn.Close();
                return tableReturn;
            }
            catch
            {
                return null;
            }
        }



        //SETTERS====================================================================================================================
        public bool addToDB(MundoPlayer player)
		{
            MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
            sqlConn.Open();
            MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO mundorp.players (steamid, level, xp, job, mp, rp) VALUES (" + player.steamid + ", " + player.level + ", " + player.xp + ", " + player.job + ", " + player.mp + ", " + player.rp + " ON DUPLICATE KEY UPDATE ", sqlConn);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
            return false;
		}

        public string updateById(string table, string column, int id, int valor)
        {
            try
            {

                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("UPDATE mundorp." + table + " set " + column + "=" + valor + " WHERE id=" + id.ToString(), sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManagerV2 | Atualizado o item: " +valor+ " da coluna: "+ column +" da tabela: "+table+" para: "+valor+"!";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
