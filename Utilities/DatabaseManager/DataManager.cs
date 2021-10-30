using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using SDG.Unturned;

namespace MundoRP
{
	public class DataManager
	{
        const string server = "127.0.0.1";
        const string port = "3306";
        const string user = "root";
        const string password = "mundorpadmin1";
        const string database = "mundorp";

        const string connectionString = "server=" + server + ";port=" + port + ";User Id=" + user + ";password=" + password;

        public string openDB()
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
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
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("TRUNCATE " + database + "." +table, sqlConn);
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
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT COUNT(*) FROM  " + database + "." + table, sqlConn);
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
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT Player_Username, Player_SteamId, Player_Level, Player_Xp, Player_Job, Player_Premium, Player_MP, Player_RP FROM " + database + ".server_players WHERE Player_SteamId=" + id.ToString(), sqlConn);
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
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();

                MySqlCommand sqlCmd = new MySqlCommand("SELECT Vehicles_uId, Vehicle_Battery, Vehicle_Health, Vehicle_Fuel, Vehicles_Name, Vehicles_Color, Vehicle_Id " +
                    "FROM " + database + ".server_players " +
                    "INNER JOIN mundorp.game_vehicles " +
                    "INNER JOIN mundorp.game_vehicles_data " +
                    "ON mundorp.game_vehicles.Vehicle_uId = " + database + ".game_vehicles_data.Vehicles_uId " +
                    "WHERE mundorp.game_vehicles.Vehicle_OwnerId =" + id.ToString() +
                    " AND mundorp.server_players.Player_SteamId =" + id.ToString(), sqlConn);

                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                int i = 0;
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string owner = id.ToString();
                        UInt16 vId = Convert.ToUInt16(dr.GetString(0));
                        
                        DateTime date = DateTime.Now;
                        ushort bat = Convert.ToUInt16(dr.GetString(1));
                        ushort hp = Convert.ToUInt16(dr.GetString(2));
                        ushort gas = Convert.ToUInt16(dr.GetString(3));
                        
                        string vname = dr.GetString(4);
                        string vColor= dr.GetString(5);
                        int TableId = Convert.ToUInt16(dr.GetString(6));

                        List<VehicleDebt> vDebts = GetVehicleDebts(TableId);

                        playerVehicles.Add(new GarageVehicle(TableId, owner, vId, vColor, date, bat, hp, gas, vname, vDebts));
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

        public List<VehicleDebt> GetVehicleDebts(int vehicleTableId)
		{
            MySqlConnection sqlConne = new MySqlConnection(connectionString);
            sqlConne.Open();
            MySqlCommand sqlCmd = new MySqlCommand("SELECT * FROM mundorp.game_vehicles_debt WHERE Debt_VehicleId = "+vehicleTableId, sqlConne);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dr = sqlCmd.ExecuteReader();


            List<VehicleDebt> newVehicleDebts = new List<VehicleDebt>();
            int i = 0;
            while (dr.HasRows)
            {
                while (dr.Read())
                {
                    Rocket.Core.Logging.Logger.Log(((float)Convert.ToUInt32(dr.GetString("Debt_Value"))).ToString());
                    newVehicleDebts.Add(new VehicleDebt(dr.GetString("Debt_Description"), (float)Convert.ToUInt32(dr.GetString("Debt_Value"))));
                }
                dr.NextResult();
                i++;
            }
            sqlConne.Close();
            return newVehicleDebts;
        }
        
        public ObjectManager getObjectsFromDB()
        {
            List<Aterro> Aterros = new List<Aterro>();
            List<CaixaCorreio> CaixasCorreios = new List<CaixaCorreio>();
            List<LataDeLixo> LatasDeLixo = new List<LataDeLixo>();
            List<PontoOnibus> PontosDeOnibus = new List<PontoOnibus>();
            List<Poste> Postes = new List<Poste>();

            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT * FROM " + database + ".game_objects", sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if(dr.GetString("Object_Type") == "busstop")
						{
                            PontosDeOnibus.Add(new PontoOnibus(dr.GetString("Object_Name"), (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        if(dr.GetString("Object_Type") == "dump")
						{
                            Aterros.Add(new Aterro("dump", (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        if(dr.GetString("Object_Type") == "mailbox")
						{
                            CaixasCorreios.Add(new CaixaCorreio(dr.GetString("Object_Name"), (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        if(dr.GetString("Object_Type") == "post")
						{
                            Postes.Add(new Poste(dr.GetString("Object_Name"), (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        if(dr.GetString("Object_Type") == "bin")
						{
                            LatasDeLixo.Add(new LataDeLixo((float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                    }
                    dr.NextResult();
                }
                sqlConn.Close();
                ObjectManager obManager = new ObjectManager(Aterros, CaixasCorreios, LatasDeLixo, PontosDeOnibus, Postes);
                return obManager;
            }
            catch(Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return null;
            }
        }

        public bool updateObjects(ObjectManager obj)
		{
			try
			{
                truncateTable("game_objects");
				MySqlConnection sqlConn = new MySqlConnection(connectionString);
				sqlConn.Open();
				foreach (Aterro obje in obj.Aterros)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'dump', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() +")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
				foreach (LataDeLixo obje in obj.LatasDeLixo)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('lixo', 'bin', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
				foreach (CaixaCorreio obje in obj.CaixasCorreios)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'mailbox', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
				foreach (Poste obje in obj.Postes)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'post', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
				foreach (PontoOnibus obje in obj.PontosDeOnibus)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'post', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
                sqlConn.Close();
                return true;
			}
			catch
			{
                return false;
			}
		}

        //SETTERS====================================================================================================================
        public bool updateCar(InteractableVehicle vh, int id)
		{
            MySqlConnection sqlConn = new MySqlConnection(connectionString);
            sqlConn.Open();
            MySqlCommand sqlCmd = new MySqlCommand("UPDATE " + database + ".game_vehicles SET Vehicle_Battery =" + vh.batteryCharge+", Vehicle_Health ="+vh.health+", Vehicle_Fuel ="+vh.fuel+" WHERE Vehicle_Id ="+id, sqlConn);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
            return false;
        }

        public bool addToDB(MundoPlayer player)
		{
            MySqlConnection sqlConn = new MySqlConnection(connectionString);
            sqlConn.Open();
            MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + database + ".server_players (Player_SteamId, Player_Level, Player_Xp, Player_Job, Player_MP, Player_RP) VALUES (" + player.steamid + ", " + player.level + ", " + player.xp + ", " + player.job + ", " + player.mp + ", " + player.rp + " ON DUPLICATE KEY UPDATE ", sqlConn);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
            return false;
		}
    }
}
