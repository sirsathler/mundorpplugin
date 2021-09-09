using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace MundoRP
{
	public class DataManager
	{
        public string openDB()
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();

                MySqlCommand sqlCmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS mundorp; CREATE TABLE IF NOT EXISTS mundorp.players(id INT NOT NULL AUTO_INCREMENT,steamid VARCHAR(50) NOT NULL,level INT NOT NULL DEFAULT '0',xp INT NOT NULL DEFAULT '0',job VARCHAR(20) NOT NULL DEFAULT 'Desempregado', PRIMARY KEY(id));", sqlConn);
                sqlCmd.ExecuteNonQuery();
                
                sqlCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS mundorp.busstops(id INT NOT NULL AUTO_INCREMENT,name VARCHAR(50) NOT NULL,type VARCHAR(50) NOT NULL,posx FLOAT NOT NULL,posy FLOAT NOT NULL,posz FLOAT NOT NULL,PRIMARY KEY(id), UNIQUE INDEX `NAME` (`name`));", sqlConn);
                sqlCmd.ExecuteNonQuery();   
                
                sqlCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS mundorp.garbages(`id` INT NOT NULL AUTO_INCREMENT,`posx` FLOAT NOT NULL,`posy` FLOAT NOT NULL,`posz` FLOAT NOT NULL,PRIMARY KEY(`id`))", sqlConn);
                sqlCmd.ExecuteNonQuery();           
                
                sqlCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS mundorp.dumps(`id` INT NOT NULL AUTO_INCREMENT,`posx` FLOAT NOT NULL,`posy` FLOAT NOT NULL,`posz` FLOAT NOT NULL,PRIMARY KEY(`id`))", sqlConn);
                sqlCmd.ExecuteNonQuery();

                sqlCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS mundorp.posts(`id` INT NOT NULL AUTO_INCREMENT, `name` VARCHAR(50) NOT NULL, `posx` FLOAT NOT NULL,`posy` FLOAT NOT NULL,`posz` FLOAT NOT NULL,PRIMARY KEY(`id`), UNIQUE INDEX `NAME` (`name`))", sqlConn);
                sqlCmd.ExecuteNonQuery();

                sqlCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS mundorp.mailbox(`id` INT NOT NULL AUTO_INCREMENT, `name` VARCHAR(50) NOT NULL, `posx` FLOAT NOT NULL,`posy` FLOAT NOT NULL,`posz` FLOAT NOT NULL,PRIMARY KEY(`id`), UNIQUE INDEX `NAME` (`name`))", sqlConn);
                sqlCmd.ExecuteNonQuery();

                sqlCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS mundorp.jobs (`id` INT NOT NULL AUTO_INCREMENT,`name` VARCHAR(50) NOT NULL,`payment` FLOAT NOT NULL,`maxLoad` INT NOT NULL,`maxPlayers` INT NOT NULL,`vehicleId` INT NOT NULL,PRIMARY KEY(`id`))", sqlConn);
                sqlCmd.ExecuteNonQuery();

                sqlConn.Close();
                return "MundoRP_DatabaseManagerV2 | Banco de Dados aberto com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public string addBusStop(PontoOnibus po)
		{
            
            decimal posx = Math.Round(Convert.ToDecimal(po.x), 0);
            decimal posy = Math.Round(Convert.ToDecimal(po.y), 0);
            decimal posz = Math.Round(Convert.ToDecimal(po.z), 0);
            
			try
			{
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO mundorp.busstops(`name`, `type`, `posx`, `posy`, `posz`) VALUES('"+po.nome+"', '"+po.tipo+"', '"+ (float)posx+"', '"+ (float)posy +"', '"+ (float)posz +"')", sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManager | Parada de ônibus adicionada no banco de dados!";
			}
            catch(Exception ex)
			{
                return ex.ToString();
			}

        }
        public string addMailbox(CaixaCorreio cx)
        {

            decimal posx = Math.Round(Convert.ToDecimal(cx.x), 0);
            decimal posy = Math.Round(Convert.ToDecimal(cx.y), 0);
            decimal posz = Math.Round(Convert.ToDecimal(cx.z), 0);

            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO mundorp.mailbox(`name`, `posx`, `posy`, `posz`) VALUES('" + cx.nome + "', '" + (float)posx + "', '" + (float)posy + "', '" + (float)posz + "')", sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManager | Caixa de Correios adicionada no banco de dados!!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        public string addPosts(Poste po)
        {

            decimal posx = Math.Round(Convert.ToDecimal(po.x), 0);
            decimal posy = Math.Round(Convert.ToDecimal(po.y), 0);
            decimal posz = Math.Round(Convert.ToDecimal(po.z), 0);

            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO mundorp.posts(`name`, `posx`, `posy`, `posz`) VALUES('" + po.nome + "', '" + (float)posx + "', '" + (float)posy + "', '" + (float)posz + "')", sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManager | Poste adicionado no banco de dados!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        public string addDump(Aterro at)
        {

            decimal posx = Math.Round(Convert.ToDecimal(at.x), 0);
            decimal posy = Math.Round(Convert.ToDecimal(at.y), 0);
            decimal posz = Math.Round(Convert.ToDecimal(at.z), 0);

            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO mundorp.dumps(`id`, `posx`, `posy`, `posz`) VALUES('" + at.id + "', '" + (float)posx + "', '" + (float)posy + "', '" + (float)posz + "')", sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManager | Aterro Sanitário adicionado no banco de dados!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        public string addGarbage(LataDeLixo lx)
		{
            
            decimal posx = Math.Round(Convert.ToDecimal(lx.x), 0);
            decimal posy = Math.Round(Convert.ToDecimal(lx.y), 0);
            decimal posz = Math.Round(Convert.ToDecimal(lx.z), 0);
            
			try
			{                
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO mundorp.garbages(`id`, `posx`, `posy`, `posz`) VALUES('"+lx.id+"', '"+ (float)posx+"', '"+ (float)posy +"', '"+ (float)posz +"')", sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManager | Lata de lixo adicionada ao banco de dados!";
			}
            catch(Exception ex)
			{
                return ex.ToString();
			}
        }
        public string truncateTable(string table)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("TRUNCATE mundorp."+table, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                return "MundoRP_DatabaseManager | Tabela "+table+" esvaziada!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public int count(string table)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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

        public Player getPlayerBySteamId(Steamworks.CSteamID id)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT username, steamid, level, xp, job, premium, mp, rp FROM mundorp.players WHERE steamid=" + id.ToString(), sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr = sqlCmd.ExecuteReader();
                dr.Read();
                
                //CRIANDO NOVO OBJETO USUÁRIO=====================
                string username, steamid, job;
                int level, xp;
                bool premium = false;
                float mp, rp;

                username = dr.GetString(0);
                steamid = dr.GetString(1);
                job = dr.GetString(4);
                level = Convert.ToInt32(dr.GetString(2));
                xp = Convert.ToInt32(dr.GetString(3));

                if (Convert.ToDateTime(dr.GetString(5)) > DateTime.Now)
                {
                    premium = true;
                }
                mp = (float)Convert.ToDouble(dr.GetString(6));
                rp = (float)Convert.ToDouble(dr.GetString(7));
                Player player = new Player(username, steamid, level, xp, job, premium, rp, mp);

                sqlConn.Close();  // FECHANDO A CONEXÃO COM O BANCO
                return player;

            }
            catch(Exception ex)
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
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
                sqlConn.Open();
                MySqlCommand sqlCmd = new MySqlCommand("SELECT name, type, posx, posy, posz FROM mundorp.posts WHERE tipo="+type, sqlConn);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dr;
                dr = sqlCmd.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PontoOnibus newBusstop = new PontoOnibus(dr.GetString(0), dr.GetString(1), (float)Convert.ToDouble(dr.GetString(2)), (float)Convert.ToDouble(dr.GetString(3)), (float)Convert.ToDouble(dr.GetString(4)));
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
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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
        public string updateById(string table, string column, int id, int valor)
        {
            try
            {
                MySqlConnection sqlConn = new MySqlConnection("server=127.0.0.1" + ";port=3306" + ";User Id=root" + ";password=mundorpadmin1");
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

public class PlayerResponse
{
    
}
