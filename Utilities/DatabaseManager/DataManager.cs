using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using SDG.Unturned;

namespace MundoRP
{
	public class DataManager
	{
        public static string connectionString = "server=" + Environments.server + ";port=" + Environments.port + ";User Id=" + Environments.user + ";password=" + Environments.password;

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
        public static void truncateTable(string table)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("TRUNCATE " + Environments.database + "." +table, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return;
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
            }
        }
        public static int count(string table)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT COUNT(*) FROM  " + Environments.database + "." + table, sqlConn);
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

        public static MundoPlayer getPlayerBySteamId(Steamworks.CSteamID id)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT Player_Level, Player_Xp, Player_Job, Player_Premium, Player_MP, Player_RP FROM " + Environments.database + ".server_players WHERE Player_SteamId=" + id.ToString(), sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                dr.Read();

                bool premium;
                premium = Convert.ToDateTime(dr.GetString(3)) > DateTime.Now ? true : false;
                List<GarageVehicle> garageVehicles = getVehiclesBySteamId(id);
                MundoPlayer player = new MundoPlayer(id, Convert.ToInt32(dr.GetString(0)), Convert.ToInt32(dr.GetString(1)), dr.GetString(2), premium, (float)Convert.ToDouble(dr.GetString(4)), (float)Convert.ToDouble(dr.GetString(5)), garageVehicles);
                sqlConn.Close();

                Rocket.Core.Logging.Logger.Log("Succesfully created MundoPlayer with steamid of: " + player.steamid.ToString());
                return player;

            }
            catch(Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return null;
            }
        }

        public static List<GarageVehicle> getVehiclesBySteamId(Steamworks.CSteamID id)
		{
            List<GarageVehicle> playerVehicles = new List<GarageVehicle>();
            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();

                MySqlCommand sqlCmd = new MySqlCommand("SELECT Vehicles_uId, Vehicle_Battery, Vehicle_Health, Vehicle_Fuel, Vehicles_Name, Vehicles_Color, Vehicle_Id " +
                    "FROM " + Environments.database + ".server_players " +
                    "INNER JOIN "+Environments.database+".server_vehicles " +
                    "INNER JOIN " + Environments.database + ".game_vehicles_data " +
                    "ON " + Environments.database + ".server_vehicles.Vehicle_uId = " + Environments.database + ".game_vehicles_data.Vehicles_uId " +
                    "WHERE " + Environments.database + ".server_vehicles.Vehicle_OwnerId =" + id.ToString() +
                    " AND " + Environments.database + ".server_players.Player_SteamId =" + id.ToString(), sqlConn);

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

                        List<VehicleDebt> vDebts = getVehicleDebts(TableId);

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

        public static List<VehicleDebt> getVehicleDebts(int vehicleTableId)
		{
            MySqlConnection sqlConne = new MySqlConnection(connectionString);
            sqlConne.Open();
            MySqlCommand sqlCmd = new MySqlCommand("SELECT * FROM "+Environments.database+".game_vehicles_debt WHERE Debt_VehicleId = "+vehicleTableId, sqlConne);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dr = sqlCmd.ExecuteReader();


            List<VehicleDebt> newVehicleDebts = new List<VehicleDebt>();
            int i = 0;
            while (dr.HasRows)
            {
                while (dr.Read())
                {
                    newVehicleDebts.Add(new VehicleDebt(dr.GetString("Debt_Description"), (float)Convert.ToUInt32(dr.GetString("Debt_Value"))));
                }
                dr.NextResult();
                i++;
            }
            sqlConne.Close();
            return newVehicleDebts;
        }
        
        public static List<Job> getJobsFromDB()
		{
			try
			{
                MySqlConnection sqlConne = new MySqlConnection(connectionString);
                sqlConne.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT * FROM " + Environments.database + ".game_jobs", sqlConne);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();


                List<Job> newJobList = new List<Job>();
                int i = 0;
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        newJobList.Add(new Job(dr.GetString("Job_Name"), (float)Convert.ToUInt32(dr.GetString("Job_Salary")), dr.GetString("Job_Description"), Convert.ToInt32(dr.GetString("Job_MinLvl")), dr.GetString("Job_Difficulty"), dr.GetString("Job_Arg0"), dr.GetString("Job_Arg1"), dr.GetString("Job_Arg2"), dr.GetString("Job_Arg3")));
                    }
                    dr.NextResult();
                    i++;
                }
                sqlConne.Close();
                return newJobList;
			}
            catch(Exception ex)
			{
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return null;
			}
        }
        
        public static ObjectManager getObjectsFromDB()
        {
            List<Dump> Dumps = new List<Dump>();
            List<Mailbox> MailBoxes = new List<Mailbox>();
            List<Garbage> Garbages = new List<Garbage>();
            List<BusStop> BusStops = new List<BusStop>();
            List<FuseBox> FuseBoxes = new List<FuseBox>();

            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT * FROM " + Environments.database + ".game_objects", sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        
                        if(dr.GetString("Object_Type") == "busstop")
						{
                            BusStops.Add(new BusStop(dr.GetString("Object_Name"), (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        else if(dr.GetString("Object_Type") == "dump")
						{
                            Dumps.Add(new Dump("dump", (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        else if(dr.GetString("Object_Type") == "mailbox")
						{
                            MailBoxes.Add(new Mailbox(dr.GetString("Object_Name"), (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        else if(dr.GetString("Object_Type") == "post")
						{
                            FuseBoxes.Add(new FuseBox(dr.GetString("Object_Name"), (float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                        else if(dr.GetString("Object_Type") == "bin")
						{
                            Garbages.Add(new Garbage((float)Convert.ToInt32(dr.GetString("Object_PosX")), (float)Convert.ToInt32(dr.GetString("Object_PosY")), (float)Convert.ToInt32(dr.GetString("Object_PosZ"))));
						}
                    }
                    dr.NextResult();
                }
                sqlConn.Close();
                ObjectManager obManager = new ObjectManager(Dumps, MailBoxes, Garbages, BusStops, FuseBoxes);
                return obManager;
            }
            catch(Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return null;
            }
        }

        public static bool updateObjects()
		{
            ObjectManager obj = new ObjectManager(Main.Instance.ObjList_Dumps, Main.Instance.ObjList_Mailbox, Main.Instance.ObjList_Garbages, Main.Instance.ObjList_BusStops, Main.Instance.ObjList_Fuses);

            try
			{
                truncateTable("game_objects");
				MySqlConnection sqlConn = new MySqlConnection(connectionString);
				sqlConn.Open();
				foreach (Dump obje in obj.Dumps)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + Environments.database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'dump', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() +")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
				foreach (Garbage obje in obj.Garbages)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + Environments.database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('lixo', 'bin', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
                foreach (Mailbox obje in obj.MailBoxes)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + Environments.database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'mailbox', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
                foreach (FuseBox obje in obj.FuseBoxes)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + Environments.database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'post', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
					sqlCmd.CommandType = System.Data.CommandType.Text;
					sqlCmd.ExecuteNonQuery();
				}
				foreach (BusStop obje in obj.BusStops)
				{
					MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO " + Environments.database + ".game_objects (Object_Name, Object_Type, Object_PosX, Object_PosY, Object_PosZ) VALUES ('" + obje.name + "', 'post', " + obje.x.ToString() + ", " + obje.y.ToString() + ", " + obje.z.ToString() + ")", sqlConn);
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

        public static void updatePlayer(MundoPlayer player)
        {
            runSQLCommand("UPDATE "+ Environments.database +".server_players SET Player_Job ='" +player.jobName+ "', Player_Level ='" +player.level+ "', Player_Xp ='" +player.xp+ "' WHERE Player_SteamId =" +player.steamid);
            return;
        }

        public static bool updateCar(InteractableVehicle vh, int id)
		{
            runSQLCommand("UPDATE " + Environments.database + ".server_vehicles SET Vehicle_Battery =" + vh.batteryCharge + ", Vehicle_Health =" + vh.health + ", Vehicle_Fuel =" + vh.fuel + " WHERE Vehicle_Id =" + id);
            return false;
        }

        public static bool addToDB(MundoPlayer player)
		{
            runSQLCommand("INSERT INTO " + Environments.database + ".server_players (Player_SteamId, Player_Level, Player_Xp, Player_Job, Player_MP, Player_RP) VALUES (" + player.steamid + ", " + player.level + ", " + player.xp + ", " + player.jobName + ", " + player.mp + ", " + player.rp + " ON DUPLICATE KEY UPDATE ");
            return false;
		}



        private static bool runSQLCommand(string sqlCommand)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection(connectionString);
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand(sqlCommand, sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch(Exception ex)
            {
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return false;
            }

        }
    }
}
