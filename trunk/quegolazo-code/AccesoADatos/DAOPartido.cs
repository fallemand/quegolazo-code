using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;


namespace AccesoADatos
{
   public class DAOPartido
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Partido de las Fechas de un Partido, es parte de una transaccion al registrar Fase.
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="fase">Fase que se esta grabando</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public void registrarPartidos(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Grupo g in fase.grupos)
                {
                    foreach (Fecha f in g.fechas)
                    {
                         foreach (Partido p in f.partidos)
                         {
                             
                                 string sql = @"INSERT INTO Partidos (idFecha,idGrupo,idFase,idEdicion,idEquipoLocal,idEquipoVisitante, idEstado)
                                     VALUES (@idFecha,@idGrupo,@idFase,@idEdicion,@idEquipoLocal,@idEquipoVisitante, @idEstado)";
                                 cmd.Parameters.Clear();
                                 //cmd.Parameters.AddWithValue("@idPartido", p.idPartido);
                                 cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                                 cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                                 cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                                 cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                                 if (p.local.idEquipo != 0)
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoLocal", p.local.idEquipo);
                                 }
                                 else
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoLocal", DBNull.Value);
                                 }
                                 if (p.visitante.idEquipo != 0)
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoVisitante", p.visitante.idEquipo);
                                 }
                                 else
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoVisitante", DBNull.Value);
                                 }
                                 cmd.Parameters.AddWithValue("@idEstado", 10);
                                 cmd.CommandText = sql;
                                 cmd.ExecuteNonQuery();
                             
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el partido" + ex.Message);
            }
          
        }

        public void obtenerPartidos(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
             try
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 cmd.Connection = con;
                 cmd.Transaction = trans;
                 foreach (Grupo grupo in fase.grupos)
                 {
                     foreach (Fecha faseActual in grupo.fechas)
                     {
                             cmd.Parameters.Clear();
                             cmd.Connection = con;
                             string sql = @"SELECT p.idPartido, p. idEstado, p.idEquipoLocal, p.idEquipoVisitante, p.idEquipoLocal, p.idEquipoVisitante, e.nombre AS 'local', e1.nombre AS 'visitante',
                                                    p.golesLocal, p.golesVisitante, p.huboPenales, p.penalesLocal, p.penalesVisitante
                                                    FROM Partidos p, Equipos e, Equipos e1
                                                    WHERE idFecha=@idFecha AND idGrupo=@idGrupo AND idFase=@idFase AND idEdicion=@idEdicion AND e.idEquipo=p.idEquipolocal AND e1.idEquipo=p.idEquipoVisitante ";
                             cmd.Parameters.AddWithValue("@idFecha", faseActual.idFecha);
                             cmd.Parameters.AddWithValue("@idGrupo", grupo.idGrupo);
                             cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                             cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                             cmd.CommandText = sql;
                             dr = cmd.ExecuteReader();
                             while (dr.Read())
                             {
                                 Partido partido = new Partido()
                                 {
                                    
                                    idPartido = int.Parse(dr["idPartido"].ToString()),
                                    // fecha = DateTime.Parse( dr["fecha"].ToString()).Date.ToString(),
                                    //hora = DateTime.Parse(dr["fecha"].ToString()).Hour.ToString(),
                                    estado= new Estado(){ idEstado= int.Parse(dr["idEstado"].ToString())},
                                    local = new Equipo() { idEquipo = int.Parse(dr["idEquipoLocal"].ToString()), nombre = dr["local"].ToString() },
                                    visitante = new Equipo() { idEquipo = int.Parse(dr["idEquipoVisitante"].ToString()), nombre = dr["visitante"].ToString() }, 
                                 };
                                 if (dr["golesLocal"] != System.DBNull.Value)
                                    partido.golesLocal=int.Parse(dr["golesLocal"].ToString());
                                 if (dr["golesVisitante"] != System.DBNull.Value)
                                    partido.golesVisitante = int.Parse(dr["golesVisitante"].ToString());
                                 if (dr["huboPenales"] != System.DBNull.Value)
                                 {
                                     partido.huboPenales = bool.Parse(dr["huboPenales"].ToString());
                                     if (dr["penalesLocal"] != System.DBNull.Value)
                                         partido.penalesLocal = int.Parse(dr["penalesLocal"].ToString());
                                     if (dr["penalesVisitante"] != System.DBNull.Value)
                                         partido.penalesVisitante = int.Parse(dr["penalesVisitante"].ToString());
                                 }
                                 faseActual.partidos.Add(partido);
                             }
                             if (dr != null)
                                 dr.Close();  
                         
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("No se pudo obtener los datos del partido" + ex.Message);
             }
             finally
             {
                 if (con != null && con.State == ConnectionState.Open)
                     con.Close();
             }
        }

        public void registrarGol(Gol gol, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Goles (minuto, idJugador, idEquipo, idPartido, idTipoGol)
                                    VALUES (@minuto, @idJugador, @idEquipo, @idPartido, @idTipoGol)";
                cmd.Parameters.Clear();
                if (gol.jugador != null)
                    cmd.Parameters.AddWithValue("@idJugador", gol.jugador.idJugador);
                else
                    cmd.Parameters.AddWithValue("@idJugador", DBNull.Value);
                if (gol.equipo != null)
                    cmd.Parameters.AddWithValue("@idEquipo", gol.equipo.idEquipo);
                else
                    cmd.Parameters.AddWithValue("@idEquipo", DBNull.Value);
                if(gol.tipoGol != null)
                    cmd.Parameters.AddWithValue("@idTipoGol", gol.tipoGol.idTipoGol);
                else
                    cmd.Parameters.AddWithValue("@idTipoGol", DBNull.Value);
                if (gol.minuto != null)
                    cmd.Parameters.AddWithValue("@minuto", gol.minuto);
                else
                    cmd.Parameters.AddWithValue("@minuto", DBNull.Value);
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el gol: " + ex.Message);
            }
        }

        public void registrarCambio(Cambio cambio, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Cambios (idEquipo, idJugadorEntra, idJugadorSale, minuto, idPartido)
                                    VALUES (@idEquipo, @idJugadorEntra, @idJugadorSale, @minuto, @idPartido)";
                cmd.Parameters.Clear();                
                cmd.Parameters.AddWithValue("@idEquipo", cambio.equipo.idEquipo);
                cmd.Parameters.AddWithValue("@idJugadorEntra", cambio.jugadorEntra.idJugador);
                cmd.Parameters.AddWithValue("@idJugadorSale", cambio.jugadorSale.idJugador);
                if(cambio.minuto != null)
                    cmd.Parameters.AddWithValue("@minuto", cambio.minuto);  
                else
                    cmd.Parameters.AddWithValue("@minuto", DBNull.Value); 
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el cambio: " + ex.Message);
            }
        }

        public void registrarTarjeta(Tarjeta tarjeta, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Tarjetas (idEquipo, idJugador, tipoTarjeta, minuto, idPartido)
                                    VALUES (@idEquipo, @idJugador, @tipoTarjeta, @minuto, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", tarjeta.equipo.idEquipo);
                cmd.Parameters.AddWithValue("@idJugador", tarjeta.jugador.idJugador);
                cmd.Parameters.AddWithValue("@tipoTarjeta", tarjeta.tipoTarjeta);
                if (tarjeta.minuto != null)
                    cmd.Parameters.AddWithValue("@minuto", tarjeta.minuto);
                else
                    cmd.Parameters.AddWithValue("@minuto", DBNull.Value);
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la tarjeta: " + ex.Message);
            }
        }

        public int registrarSancion(Sancion sancion, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Sanciones (idEquipo, idJugador, motivo, idPartido)
                                    VALUES (@idEquipo, @idJugador, @motivo, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", sancion.idEquipo);                
                if(sancion.idJugador != null)
                    cmd.Parameters.AddWithValue("@idJugador", sancion.idJugador);
                else
                    cmd.Parameters.AddWithValue("@idJugador", DBNull.Value);                
                if (sancion.motivo != null)
                    cmd.Parameters.AddWithValue("@motivo", sancion.motivo);
                else
                    cmd.Parameters.AddWithValue("@motivo", DBNull.Value);
                if(idPartido != null)
                    cmd.Parameters.AddWithValue("@idPartido", idPartido);
                else
                    cmd.Parameters.AddWithValue("@idPartido", DBNull.Value); 
                cmd.CommandText = sql;
                int idSancion = int.Parse(cmd.ExecuteScalar().ToString());
                return idSancion; //retorna el id de la sanción generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la sanción: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void registrarTitularesAPartido(List<Jugador> jugadores, int idEquipo, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Jugador jugador in jugadores)
                {
                    string sql = @"INSERT INTO TitularesXPartido (idPartido, idJugador, idEquipo)
                                    VALUES (@idPartido, @idJugador, @idEquipo)
                                    SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idPartido", idPartido);
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                    cmd.Parameters.AddWithValue("@idJugador", jugador.idJugador);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo registrar los jugadores titulares: " + ex.Message);
            }
        }

       /// <summary>
       /// Obtiene un partido por Id 
       /// autor: Pau Pedrosa
       /// </summary>
       /// <param name="idPartido">Id de Partido a obtener</param>
       /// <returns>
       /// Objeto Partido
       /// Devuelve: Equipos Local y Visitante - Árbitro - Cancha - Id, Hora y fecha del partido
       /// </returns>
        public Partido obtenerPartidoPorId(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Partido partido = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT p.idPartido AS 'idPartido', p.golesLocal, p.golesVisitante, p.huboPenales, p.penalesLocal,
                                p.penalesVisitante, e1.idEquipo AS 'idEquipoLocal', e1.nombre AS 'nombreLocal',
                                e2.idEquipo AS 'idEquipoVisitante', e2.nombre AS 'nombreVisitante', a.idArbitro AS 'idArbitro',
                                a.nombre AS 'nombreArbitro', c.idCancha AS 'idCancha', c.nombre AS 'nombreCancha', e.idEstado AS 'idEstado',
                                e.nombre AS 'nombreEstado', p.fecha AS 'fecha'
                                FROM Partidos p 
                                LEFT OUTER JOIN Equipos e1 ON p.idEquipoLocal = e1.idEquipo 
                                LEFT OUTER JOIN Equipos e2 ON p.idEquipoVisitante = e2.idEquipo
                                LEFT OUTER JOIN Arbitros a ON p.idArbitro = a.idArbitro 
                                LEFT OUTER JOIN Canchas c ON p.idCancha = c.idCancha 
                                LEFT OUTER JOIN Estados e ON e.idEstado = p.idEstado
                                WHERE p.idPartido = @idPartido";
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    partido = new Partido();
                    partido.idPartido = Int32.Parse(dr["idPartido"].ToString());
                    partido.local.idEquipo = Int32.Parse(dr["idEquipoLocal"].ToString());
                    partido.local.nombre = dr["nombreLocal"].ToString();
                    partido.visitante.idEquipo = Int32.Parse(dr["idEquipoVisitante"].ToString());
                    partido.visitante.nombre = dr["nombreVisitante"].ToString();
                    if (dr["golesLocal"] != System.DBNull.Value)
                        partido.golesLocal = Int32.Parse(dr["golesLocal"].ToString());
                    else
                        partido.golesLocal = null;
                    if (dr["golesVisitante"] != System.DBNull.Value)
                        partido.golesVisitante = Int32.Parse(dr["golesVisitante"].ToString());
                    else
                        partido.golesVisitante = null;
                    if (dr["huboPenales"] != System.DBNull.Value)
                        partido.huboPenales = bool.Parse(dr["huboPenales"].ToString());
                    else
                        partido.huboPenales = null;
                    if (dr["penalesLocal"] != System.DBNull.Value)
                        partido.penalesLocal = Int32.Parse(dr["penalesLocal"].ToString());
                    else
                        partido.penalesLocal = null;
                    if (dr["penalesVisitante"] != System.DBNull.Value)
                        partido.penalesVisitante = Int32.Parse(dr["penalesVisitante"].ToString());
                    else
                        partido.penalesVisitante = null;
                    if (dr["idArbitro"] != System.DBNull.Value)
                    {
                        partido.arbitro.idArbitro = Int32.Parse(dr["idArbitro"].ToString());
                        partido.arbitro.nombre = dr["nombreArbitro"].ToString();
                    }
                    else
                        partido.arbitro = null;
                    if (dr["idCancha"] != System.DBNull.Value)
                    {
                        partido.cancha.idCancha = Int32.Parse(dr["idCancha"].ToString());
                        partido.cancha.nombre = dr["nombreCancha"].ToString();
                    }
                    else
                        partido.cancha = null;                     
                    partido.estado.idEstado = Int32.Parse(dr["idEstado"].ToString());
                    partido.estado.nombre = dr["nombreEstado"].ToString();
                    if (dr["fecha"] != System.DBNull.Value)
                        partido.fecha = DateTime.Parse(dr["fecha"].ToString());
                    else
                        partido.fecha = null;
                }
                if (dr != null)
                    dr.Close();
                return partido;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Los Goles de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Gol</returns>
        public List<Gol> obtenerGoles(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Gol> goles = new List<Gol>();
            Gol gol = null; 
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT g.idGol AS 'idGol', tg.nombre AS 'tipoGol', tg.idTipoGol, g.minuto AS 'minuto',
                                g.idPartido AS 'idPartido', g.idJugador AS 'idJugador', g.idEquipo AS 'idEquipo', j.nombre as 'nombreJugador'
                                FROM Goles g 
                                INNER JOIN TiposGol tg ON tg.idTipoGol = g.idTipoGol
                                INNER JOIN Jugadores j ON g.idJugador = j.idJugador
                                WHERE g.idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    gol = new Gol();
                    gol.idGol = Int32.Parse(dr["idGol"].ToString());
                    if (dr["minuto"] != System.DBNull.Value)
                        gol.minuto = Int32.Parse(dr["minuto"].ToString());
                    else
                        gol.minuto = null;
                    if (dr["idJugador"] != System.DBNull.Value)
                    {
                        gol.jugador.idJugador = Int32.Parse(dr["idJugador"].ToString());
                        gol.jugador.nombre = dr["nombreJugador"].ToString();
                    }
                    else
                        gol.jugador = null;
                    if (dr["idEquipo"] != System.DBNull.Value)
                        gol.equipo.idEquipo = Int32.Parse(dr["idEquipo"].ToString());
                    else
                        gol.equipo = null;
                    if (dr["tipoGol"] != System.DBNull.Value)
                        gol.tipoGol.nombre = dr["tipoGol"].ToString();
                    else
                        gol.tipoGol = null;
                    if (dr["idTipoGol"] != System.DBNull.Value)
                        gol.tipoGol.idTipoGol = Int32.Parse(dr["idTipoGol"].ToString());
                    goles.Add(gol);
                }
                if (dr != null)
                    dr.Close();
                return goles;            
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los goles del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Las Tarjetas de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Tarjeta</returns>
        public List<Tarjeta> obtenerTarjetas(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            Tarjeta tarjeta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT t.*, j.nombre as 'nombreJugador'
                                FROM Tarjetas t, Jugadores j
                                WHERE t.idJugador=j.idJugador AND idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tarjeta = new Tarjeta();
                    tarjeta.idTarjeta = Int32.Parse(dr["idTarjeta"].ToString());
                    if (dr["minuto"] != System.DBNull.Value)
                        tarjeta.minuto = Int32.Parse(dr["minuto"].ToString());
                    else
                        tarjeta.minuto = null;
                    tarjeta.jugador.idJugador = Int32.Parse(dr["idJugador"].ToString());
                    tarjeta.jugador.nombre = dr["nombreJugador"].ToString();
                    tarjeta.equipo.idEquipo = Int32.Parse(dr["idEquipo"].ToString());
                    tarjeta.tipoTarjeta = char.Parse(dr["tipoTarjeta"].ToString());
                    tarjetas.Add(tarjeta);
                }
                if (dr != null)
                    dr.Close();
                return tarjetas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las tarjetas del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Los Cambios de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Cambio</returns>
        public List<Cambio> obtenerCambios(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Cambio> cambios = new List<Cambio>();
            Cambio cambio = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT c.*, je.nombre as 'jugadorEntra', js.nombre as 'jugadorSale' 
                                FROM Cambios c, Jugadores je, Jugadores js 
                                WHERE idPartido = @idPartido AND je.idJugador = c.idJugadorEntra AND js.idJugador = c.idJugadorSale";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cambio = new Cambio();
                    cambio.idCambio = Int32.Parse(dr["idCambio"].ToString());
                    if (dr["minuto"] != System.DBNull.Value)
                        cambio.minuto = Int32.Parse(dr["minuto"].ToString());
                    else
                        cambio.minuto = null;                    
                    cambio.jugadorEntra.idJugador = Int32.Parse(dr["idJugadorEntra"].ToString());
                    cambio.jugadorEntra.nombre = dr["jugadorEntra"].ToString();
                    cambio.jugadorSale.idJugador = Int32.Parse(dr["idJugadorSale"].ToString());
                    cambio.jugadorSale.nombre = dr["jugadorSale"].ToString();
                    cambio.equipo.idEquipo = Int32.Parse(dr["idEquipo"].ToString());
                    cambios.Add(cambio);
                }
                if (dr != null)
                    dr.Close();
                return cambios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los cambios del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Los Titulares de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Jugador</returns>
        public List<Jugador> obtenerTitularesDeUnPartido(int idPartido, int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Jugador> titulares = new List<Jugador>();
            Jugador jugador = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Jugadores j 
                                INNER JOIN TitularesXPartido txp ON j.idJugador = txp.idJugador
                                WHERE txp.idPartido = @idPartido AND txp.idEquipo = @idEquipo";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    jugador = new Jugador();
                    jugador.idJugador = Int32.Parse(dr["idJugador"].ToString());
                    jugador.nombre = dr["nombre"].ToString();
                    jugador.dni = dr["dni"].ToString();
                    if (dr["fechaNacimiento"] != System.DBNull.Value)
                        jugador.fechaNacimiento = DateTime.Parse(dr["fechaNacimiento"].ToString());
                    else
                        jugador.fechaNacimiento = null;
                    jugador.email = dr["email"].ToString();
                    jugador.facebook = dr["facebook"].ToString();
                    jugador.sexo = dr["sexo"].ToString();
                    jugador.telefono = dr["telefono"].ToString();
                    if (dr["numeroCamiseta"] != System.DBNull.Value)
                        jugador.numeroCamiseta = Int32.Parse(dr["numeroCamiseta"].ToString());
                    else
                        jugador.numeroCamiseta = null;
                    jugador.tieneFichaMedica = bool.Parse(dr["tieneFichaMedica"].ToString());
                    titulares.Add(jugador);
                }
                if (dr != null)
                    dr.Close();
                return titulares;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los titulares del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void modificarPartido(Partido partido)
        {
            eliminarTitularesDePartido(partido.idPartido);
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"UPDATE Partidos
                                SET fecha = @fecha,
                                idEstado = @idEstado, idArbitro = @idArbitro, idCancha = @idCancha, golesLocal = @golesLocal, 
                                huboPenales = @huboPenales, penalesLocal = @penalesLocal, penalesVisitante = @penalesVisitante,
                                golesVisitante = @golesVisitante, idGanador = @idGanador, idPerdedor = @idPerdedor, empate = @empate
                                WHERE idPartido = @idPartido ";
                cmd.Parameters.Clear();
                if (partido.fecha != null)
                    cmd.Parameters.AddWithValue("@fecha", partido.fecha);
                else
                    cmd.Parameters.AddWithValue("@fecha", DBNull.Value);
                cmd.Parameters.AddWithValue("@idEstado", partido.estado.idEstado);
                if (partido.arbitro != null)
                    cmd.Parameters.AddWithValue("@idArbitro", partido.arbitro.idArbitro);
                else
                    cmd.Parameters.AddWithValue("@idArbitro", DBNull.Value);
                if (partido.cancha != null)
                    cmd.Parameters.AddWithValue("@idCancha", partido.cancha.idCancha);
                else
                    cmd.Parameters.AddWithValue("@idCancha", DBNull.Value);
                if (partido.golesLocal != null)
                    cmd.Parameters.AddWithValue("@golesLocal", partido.golesLocal);
                else
                    cmd.Parameters.AddWithValue("@golesLocal", DBNull.Value);
                if (partido.golesVisitante != null)
                    cmd.Parameters.AddWithValue("@golesVisitante", partido.golesVisitante);
                else
                    cmd.Parameters.AddWithValue("@golesVisitante", DBNull.Value);
                cmd.Parameters.AddWithValue("@huboPenales", partido.huboPenales);
                if (partido.penalesLocal != null)
                    cmd.Parameters.AddWithValue("@penalesLocal", partido.penalesLocal);
                else
                    cmd.Parameters.AddWithValue("@penalesLocal", DBNull.Value);
                if (partido.penalesVisitante != null)
                    cmd.Parameters.AddWithValue("@penalesVisitante", partido.penalesVisitante);
                else
                    cmd.Parameters.AddWithValue("@penalesVisitante", DBNull.Value);
                if (partido.idGanador != null)
                    cmd.Parameters.AddWithValue("@idGanador", partido.idGanador);
                else
                    cmd.Parameters.AddWithValue("@idGanador", DBNull.Value);
                if (partido.idPerdedor != null)
                    cmd.Parameters.AddWithValue("@idPerdedor", partido.idPerdedor);
                else
                    cmd.Parameters.AddWithValue("@idPerdedor", DBNull.Value);
                if (partido.empate != null)
                    cmd.Parameters.AddWithValue("@empate", partido.empate);
                else
                    cmd.Parameters.AddWithValue("@empate", DBNull.Value);
                cmd.Parameters.AddWithValue("@idPartido", partido.idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                eliminarGolesDePartido(partido.idPartido);
                foreach (Gol gol in partido.goles)
                {
                    registrarGol(gol, partido.idPartido, con, trans);
                }
                eliminarTarjetasDePartido(partido.idPartido);
                foreach (Tarjeta tarjeta in partido.tarjetas)
                {
                    registrarTarjeta(tarjeta, partido.idPartido, con, trans);
                }
                eliminarCambiosDePartido(partido.idPartido);
                foreach (Cambio cambio in partido.cambios)
                {
                    registrarCambio(cambio, partido.idPartido, con, trans);
                }
                if(partido.local.idEquipo != null)
                    registrarTitularesAPartido(partido.titularesLocal, partido.local.idEquipo, partido.idPartido, con, trans);//titulares del equipo local
                if(partido.visitante.idEquipo != null)
                    registrarTitularesAPartido(partido.titularesVisitante, partido.visitante.idEquipo, partido.idPartido, con, trans);//titulares del equipo visitante}
                trans.Commit();
            }
            catch (Exception ex)
            {  
                trans.Rollback();
                throw new Exception("No se pudo registrar el equipo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public List<TipoGol> obtenerTiposGol()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<TipoGol> respuesta = new List<TipoGol>();
            TipoGol tipoGol = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM TiposGol";
                cmd.Parameters.Clear();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tipoGol = new TipoGol()
                    {
                        idTipoGol = Int32.Parse(dr["idTipoGol"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    respuesta.Add(tipoGol);
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina los goles de un partido de la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarGolesDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Goles
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el gol: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Eliminar los titulares de un partido
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarTitularesDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM TitularesXPartido
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar los titulares del partido: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina los cambios de un partido de la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarCambiosDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Cambios
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el cambio: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina las tarjetas de un partido de la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarTarjetasDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Tarjetas
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la tarjeta: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
