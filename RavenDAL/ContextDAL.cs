using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LumberJack;

namespace RavenDAL
{//This activates the using (ContextDAL ctx = new ContextDAL()){...}
    public class ContextDAL : IDisposable
    {
        #region Context Stuff
        SqlConnection _connection;
        public ContextDAL()
        {
            _connection = new SqlConnection();
        }
        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }
        //mention this and what it does
        void EnsureConnected()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                // there is nothing to do if I am connected
            }
            else if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            else if (_connection.State == System.Data.ConnectionState.Broken)
            {
                _connection.Close();
                _connection.Open();
            }
        }
        //logger logic
        bool Log(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Logger.Log(ex);
            return false;
        }

        public void Dispose()
        {
            //This Disposes the connection inside when someone Disposes me
            _connection.Dispose();
        }
        #endregion Context Stuff
        #region simulate Exceptions for testing
        public int GenerateNotConnected()
        {
            int ProposedReturnValue = -1;
            try
            {
                // by commenting out the EnsureConnected below, this method MAY throw an
                // exception IF it is the First call on a ContextDAL

                //EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public int GenerateParameterNotIncluded()
        {
            int proposedReturnValue = -1;
            try
            {


                EnsureConnected();
                // the parameter to the stored procdeure is incorrect, so it should throw an exception
                using (SqlCommand command = new SqlCommand("FindRoleByRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // the following line is where the parameter name is incorrect
                    command.Parameters.AddWithValue("@XXXX", 1);
                    object answer = command.ExecuteReader();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }
        #endregion testing
        #region Drones Stuff
        public DronesDAL FindDrone(int DroneID)
        {
            DronesDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindDrone", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DroneID", DroneID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DronesMapper m = new DronesMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.DroneFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Drone with key {DroneID}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {

                
            }
            return ProposedReturnValue;
        }
        public List<DronesDAL> GetDrones(int skip, int take)
        {
            List<DronesDAL> ProposedReturnValue = new List<DronesDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetDrones", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DronesMapper m = new DronesMapper(reader);
                        while (reader.Read())
                        {
                            DronesDAL r = m.DroneFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public List<DronesDAL> GetDronesRelatedToUser(int UserID, int skip, int take)
        {
            List<DronesDAL> ProposedReturnValue = new List<DronesDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetDronesRelatedToUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DronesMapper m = new DronesMapper(reader);
                        while (reader.Read())
                        {
                            DronesDAL r = m.DroneFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public int ObtainDroneCount()
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainDroneCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return ProposedReturnValue;
        }
        //Drones have a role Id because I intend to be able to add more roles in the future
        //that will involve military drones and I want to limit accessability.
        public int CreateDrone(int RoleID,string DroneName,int UserID)
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateDrone", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DroneID", 0);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@DroneName", DroneName);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    
                    command.Parameters["@DroneID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@DroneID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public void JustUpdateDrone(int DroneID,int RoleID, string DroneName, int UserID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateDrone", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DroneID", DroneID);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@DroneName", DroneName);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }
        public void DeleteDrone(int DroneID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteDrone", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DroneID", DroneID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }

        #endregion Drones Stuff
        #region History Stuff
        public HistoryDAL FindHistoryByID(int HistoryID)
        {
            HistoryDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindHistoryByID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@HistoryID", HistoryID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        HistoryMapper m = new HistoryMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.HistoryFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 History with key {HistoryID}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public List<HistoryDAL> GetHistoryByIDs(int skip, int take)
        {
            List<HistoryDAL> ProposedReturnValue = new List<HistoryDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetHistoryByIDs", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        HistoryMapper m = new HistoryMapper(reader);
                        while (reader.Read())
                        {
                            HistoryDAL r = m.HistoryFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public List<HistoryDAL> GetHistoryRelatedToViolationIDs(int ViolationID,int skip, int take)
        {
            List<HistoryDAL> ProposedReturnValue = new List<HistoryDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetHistoryRelatedToViolationIDs", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ViolationID", ViolationID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        HistoryMapper m = new HistoryMapper(reader);
                        while (reader.Read())
                        {
                            HistoryDAL r = m.HistoryFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public List<HistoryDAL> GetHistoryByPlateID(int PlateID, int skip, int take)
        {
            List<HistoryDAL> ProposedReturnValue = new List<HistoryDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetHistoryByPlateID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        HistoryMapper m = new HistoryMapper(reader);
                        while (reader.Read())
                        {
                            HistoryDAL r = m.HistoryFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public int ObtainHistoryCount()
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainHistoryCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return ProposedReturnValue;
        }
        public int CreateHistory(int PlateID, string PaidFine, string RegisteredOwner, string Address1, string State,int ViolationID)
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateHistory", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@HistoryID", 0);
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@PaidFine", PaidFine);
                    command.Parameters.AddWithValue("@RegisteredOwner", RegisteredOwner);
                    command.Parameters.AddWithValue("@Address1", Address1);
                    command.Parameters.AddWithValue("@State", State);
                    command.Parameters.AddWithValue("@ViolationID", ViolationID);
                    command.Parameters["@HistoryID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@HistoryID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public void JustUpdateHistory(int HistoryID,int PlateID, string PaidFine, string RegisteredOwner, string Address1, string State, int ViolationID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateHistory", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@HistoryID", HistoryID);
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@PaidFine", PaidFine);
                    command.Parameters.AddWithValue("@RegisteredOwner", RegisteredOwner);
                    command.Parameters.AddWithValue("@Address1", Address1);
                    command.Parameters.AddWithValue("@State", State);
                    command.Parameters.AddWithValue("@ViolationID", ViolationID);
                    

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }
        public void DeleteHistory(int HistoryID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteHistory", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@HistoryID", HistoryID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }


        #endregion History Stuff
        #region Observation Stuff
        public ObservationsDAL FindObservation(int ObsID)
        {
            ObservationsDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindObservation", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ObsID", ObsID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ObservationsMapper m = new ObservationsMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.ObservationFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Observation with key {ObsID}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public List<ObservationsDAL> GetObservationsRelatedToDroneID(int DroneID,int skip, int take)
        {
            List<ObservationsDAL> ProposedReturnValue = new List<ObservationsDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetObservationsRelatedToDroneID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DroneID", DroneID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ObservationsMapper m = new ObservationsMapper(reader);
                        while (reader.Read())
                        {
                            ObservationsDAL r = m.ObservationFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public List<ObservationsDAL> GetObservationsRelatedToObs( int skip, int take)
        {
            List<ObservationsDAL> ProposedReturnValue = new List<ObservationsDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetObservationsRelatedToObs", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ObservationsMapper m = new ObservationsMapper(reader);
                        while (reader.Read())
                        {
                            ObservationsDAL r = m.ObservationFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public int ObtainObservationsCount()
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainObservationsCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return ProposedReturnValue;
        }
        public int CreateObservation(int speed, string LatNumber, string LongNumber, int PlateID, string RegisteredOwner, int DroneID)
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateObservation", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ObsID", 0);
                    command.Parameters.AddWithValue("@speed", speed);
                    command.Parameters.AddWithValue("@LatNumber", LatNumber);
                    command.Parameters.AddWithValue("@LongNumber", LongNumber);
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@RegisteredOwner", RegisteredOwner);
                    command.Parameters.AddWithValue("@DroneID", DroneID);
                    command.Parameters["@ObsID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@ObsID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public void JustUpdateObservation(int ObsID,int speed, string LatNumber, string LongNumber, int PlateID, string RegisteredOwner, int DroneID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateObservation", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ObsID", ObsID);
                    command.Parameters.AddWithValue("@speed", speed);
                    command.Parameters.AddWithValue("@LatNumber", LatNumber);
                    command.Parameters.AddWithValue("@LongNumber", LongNumber);
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@RegisteredOwner", RegisteredOwner);
                    command.Parameters.AddWithValue("@DroneID", DroneID);
                    

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }
        public void DeleteObservation(int ObsID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteObservation", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ObsID", ObsID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }
        #endregion Observation Stuff
        #region Role Stuff
        public RolesDAL FindRoleByID(int RoleID)
        {
            RolesDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindRoleByID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper m = new RoleMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.RoleFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Role with key {RoleID}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public List<RolesDAL> GetRoles( int skip, int take)
        {
            List<RolesDAL> ProposedReturnValue = new List<RolesDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRoles", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper m = new RoleMapper(reader);
                        while (reader.Read())
                        {
                            RolesDAL r = m.RoleFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public int ObtainRoleCount()
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return ProposedReturnValue;
        }
        public int CreateRole(string RoleName)
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", 0);
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters["@RoleID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@RoleID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public void JustUpdateRole(int RoleID, string RoleName)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        public void DeleteRole(int RoleID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleID", RoleID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion Role Stuff
        #region User Stuff
        public UsersDAL FindUser(int UserID)
        {
            UsersDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                //The _connection is required for the connection to the database
                using (SqlCommand command
                    = new SqlCommand("FindUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 User with key {UserID}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public UsersDAL FindUserByEmail(string Email)
        {
            UsersDAL ProposedReturnValue = null;
            try
            {
                //talk about the command statement, the stored procedure
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindUserByEmail", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", Email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Email with key {Email}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public List<UsersDAL> GetUsers(int skip, int take)
        {
            List<UsersDAL> ProposedReturnValue = new List<UsersDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetUsers", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        while (reader.Read())
                        {
                            UsersDAL r = m.UserFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public List<UsersDAL> GetUsersRelatedToRoleID(int RoleID,int skip, int take)
        {
            List<UsersDAL> ProposedReturnValue = new List<UsersDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetUsersRelatedToRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        while (reader.Read())
                        {
                            UsersDAL r = m.UserFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public int ObtainUserCount()
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainUserCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //ExecuteScalar is called when it is only returning 1 thing
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return ProposedReturnValue;
        }
        public int CreateUser(string Email, string UserName,  string Hash, string Salt, int RoleID)
        {
            //This is an out of range value in case an exception occurs
            //Normally the return value will be the ID of the record just created
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //UserID is an output hints 0
                    command.Parameters.AddWithValue("@UserID", 0);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Hash", Hash);
                    command.Parameters.AddWithValue("@Salt", Salt);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters["@UserID"].Direction = System.Data.ParameterDirection.Output;
                    //ExecuteNonQuery doesn't call the select cammand
                    command.ExecuteNonQuery();
                    //after the execute non-query is over it exstacts the userID
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@UserID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public void JustUpdateUser(int UserID,string Email, string UserName, string Hash, string Salt, int RoleID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Hash", Hash);
                    command.Parameters.AddWithValue("@Salt", Salt);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    
                    
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        public void DeleteUser(int UserID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion User Stuff
        #region Violations Stuff
        public ViolationsDAL FindViolationByViolationID(int ViolationID)
        {
            ViolationsDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindViolationByViolationID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ViolationID", ViolationID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ViolationsMapper m = new ViolationsMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.ViolationFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Violation with key   {ViolationID}");

                        }
                    }
                }

            }
            catch (Exception ex) when (Log(ex))
            {


            }
            return ProposedReturnValue;
        }
        public List<ViolationsDAL> GetViolations(int skip, int take)
        {
            List<ViolationsDAL> ProposedReturnValue = new List<ViolationsDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetViolations", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ViolationsMapper m = new ViolationsMapper(reader);
                        while (reader.Read())
                        {
                            ViolationsDAL r = m.ViolationFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public List<ViolationsDAL> GetViolationsRelatedToObsID(int ObsID, int skip, int take)
        {
            List<ViolationsDAL> ProposedReturnValue = new List<ViolationsDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetViolationsRelatedToObsID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ObsID", ObsID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ViolationsMapper m = new ViolationsMapper(reader);
                        while (reader.Read())
                        {
                            ViolationsDAL r = m.ViolationFromReader(reader);
                            ProposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public int ObtainViolationsCount()
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainViolationsCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    ProposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return ProposedReturnValue;
        }
        public int CreateViolation(string ViolationDesc, int RecordSpeed, Decimal FineAmount, int PlateID, int ObsID)
        {
            int ProposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateViolation", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ViolationID", 0);
                    command.Parameters.AddWithValue("@ViolationDesc", ViolationDesc);
                    command.Parameters.AddWithValue("@RecordSpeed", RecordSpeed);
                    command.Parameters.AddWithValue("@FineAmount", FineAmount);
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@ObsID", ObsID);
                    command.Parameters["@ViolationID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    ProposedReturnValue =
                        Convert.ToInt32(command.Parameters["@ViolationID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;
        }
        public void JustUpdateViolation(int ViolationID,string ViolationDesc, int RecordSpeed, Decimal FineAmount, int PlateID, int ObsID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateViolation", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ViolationID", ViolationID);
                    command.Parameters.AddWithValue("@ViolationDesc", ViolationDesc);
                    command.Parameters.AddWithValue("@RecordSpeed", RecordSpeed);
                    command.Parameters.AddWithValue("@FineAmount", FineAmount);
                    command.Parameters.AddWithValue("@PlateID", PlateID);
                    command.Parameters.AddWithValue("@ObsID", ObsID);
                    
                    
                    
                    

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        //This is a cascade delete, the history will be deleted first and then the 
        //violation
        public void ViolationDelete(int ViolationID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ViolationDelete", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ViolationID", ViolationID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        #endregion Violations Stuff
    }
}