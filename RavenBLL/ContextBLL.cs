using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RavenDAL;
using LumberJack;

namespace RavenBLL
{
    #region ContextBLL Stuff
    public class ContextBLL : IDisposable
    {
        RavenDAL.ContextDAL _context = new RavenDAL.ContextDAL();
        public void Dispose()
        {
            _context.Dispose();
        }
        //This is my logging
        bool Log(Exception ex)
        {
            Console.WriteLine(ex);
            Logger.Log(ex);
            return false;
        }
        public ContextBLL()
        {
            try
            {
                //Show the default connection string
                string connectionstring;
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
                _context.ConnectionString = connectionstring;
            }
            catch (Exception ex) when(Log(ex))
            {

                //there is no reasonable handler
            }
        }
        public void GenerateNotConnected()
        {
            _context.GenerateNotConnected();
        }
        
        public void GenerateParameterNotIncluded()
        {
            _context.GenerateParameterNotIncluded();

        }
        #endregion ContextBLL Stuff
        #region RolesBLLStuff

        public RolesBLL FindRoleByID(int RoleID)
        {
            RolesBLL ProposedReturnValue = null;
            RolesDAL DataLayerObject = _context.FindRoleByID(RoleID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new RolesBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public List<RolesBLL> GetRoles(int skip, int take)
        {
            List<RolesBLL> ProposedReturnValue = new List<RolesBLL>();
            List<RolesDAL> ListOfDataLayerObjects = _context.GetRoles(skip, take);
            foreach (RolesDAL role in ListOfDataLayerObjects)
            {
                RolesBLL BusinessObject = new RolesBLL(role);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public int ObtainRoleCount()
        {
            int ProposedReturnValue = 0;
            ProposedReturnValue = _context.ObtainRoleCount();
            return ProposedReturnValue;
        }
        public int CreateRole(string RoleName)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateRole(RoleName);
            return ProposedReturnValue;
        }
        public int CreateRole(RolesBLL role)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateRole(role.RoleName);
            return ProposedReturnValue;
        }
        public void JustUpdateRole(int RoleID, string RoleName)
        {

            _context.JustUpdateRole(RoleID, RoleName);

        }
        public void JustUpdateRole(RolesBLL Role)
        {

            _context.JustUpdateRole(Role.RoleID, Role.RoleName);

        }
        public void DeleteRole(int RoleID)
        {
            _context.DeleteRole(RoleID);
        }
        public void DeleteRole(RolesBLL Role)
        {
            _context.DeleteRole(Role.RoleID);
        }
        #endregion RolesBLL Stuff
        #region DronesBLL Stuff
        public DronesBLL FindDrone(int DroneID)
        {
            DronesBLL ProposedReturnValue = null;
            DronesDAL DataLayerObject = _context.FindDrone(DroneID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new DronesBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public List<DronesBLL> GetDrones(int skip, int take)
        {
            List<DronesBLL> ProposedReturnValue = new List<DronesBLL>();
            List<DronesDAL> ListOfDataLayerObjects = _context.GetDrones(skip, take);
            foreach (DronesDAL Drone in ListOfDataLayerObjects)
            {
                DronesBLL BusinessObject = new DronesBLL(Drone);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public List<DronesBLL> GetDronesRelatedToUser(int UserID, int Skip, int Take)
        {
            List<DronesBLL> ProposedReturnValue = new List<DronesBLL>();
            List<DronesDAL> ListOfDataLayerObjects = _context.GetDronesRelatedToUser(UserID, Skip, Take);
            foreach (DronesDAL Drone in ListOfDataLayerObjects)
            {
                DronesBLL BusinessObject = new DronesBLL(Drone);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public int ObtainDroneCount()
        {
            int ProposedReturnValue = 0;
            ProposedReturnValue = _context.ObtainDroneCount();
            return ProposedReturnValue;
        }
        public int CreateDrone(int RoleID, string DroneName, int UserID)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateDrone(RoleID, DroneName, UserID);
            return ProposedReturnValue;
        }
        public int CreateDrone(DronesBLL Drones)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateDrone(Drones.RoleID, Drones.DroneName, Drones.UserID);
            return ProposedReturnValue;
        }
        public void JustUpdateDrone(int DroneID,int RoleID, string DroneName, int UserID)
        {

            _context.JustUpdateDrone(DroneID,RoleID, DroneName, UserID);

        }
        public void JustUpdateDrone(DronesBLL Drones)
        {

            _context.JustUpdateDrone(Drones.DroneID, Drones.RoleID, Drones.DroneName, Drones.UserID);

        }
        public void DeleteDrone(int DroneID)
        {
            _context.DeleteDrone(DroneID);
        }
        public void DeleteDrone(DronesBLL Drones)
        {
            _context.DeleteDrone(Drones.DroneID);
        }
        #endregion DronesBLL Stuff
        #region HistroyBLL Stuff
        public HistoryBLL FindHistoryByID(int HistoryID)
        {
            HistoryBLL ProposedReturnValue = null;
            HistoryDAL DataLayerObject = _context.FindHistoryByID(HistoryID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new HistoryBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public List<HistoryBLL> GetHistoryByIDs( int skip, int take)
        {
            List<HistoryBLL> ProposedReturnValue = new List<HistoryBLL>();
            List<HistoryDAL> ListOfDataLayerObjects = _context.GetHistoryByIDs( skip, take);
            foreach (HistoryDAL Drone in ListOfDataLayerObjects)
            {
                HistoryBLL BusinessObject = new HistoryBLL(Drone);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public List<HistoryBLL> GetHistoryRelatedToViolationIDs(int ViolationID,int skip, int take)
        {
            List<HistoryBLL> ProposedReturnValue = new List<HistoryBLL>();
            List<HistoryDAL> ListOfDataLayerObjects = _context.GetHistoryRelatedToViolationIDs(ViolationID,skip, take);
            foreach (HistoryDAL History in ListOfDataLayerObjects)
            {
                HistoryBLL BusinessObject = new HistoryBLL(History);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public List<HistoryBLL> GetHistoryByPlateID(int PlateID, int skip, int take)
        {
            List<HistoryBLL> ProposedReturnValue = new List<HistoryBLL>();
            List<HistoryDAL> ListOfDataLayerObjects = _context.GetHistoryByPlateID(PlateID, skip, take);
            foreach (HistoryDAL History in ListOfDataLayerObjects)
            {
                HistoryBLL BusinessObject = new HistoryBLL(History);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public int ObtainHistoryCount()
        {
            int ProposedReturnValue = 0;
            ProposedReturnValue = _context.ObtainHistoryCount();
            return ProposedReturnValue;
        }
        public int CreateHistory(int PlateID, string PaidFine, string RegisteredOwner, string Address1, string State, int ViolationID)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateHistory(PlateID, PaidFine, RegisteredOwner, Address1, State, ViolationID);
            return ProposedReturnValue;
        }
        public int CreateHistory(HistoryBLL History)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateHistory(History.PlateID, History.PaidFine, History.RegisteredOwner, History.Address1, History.State, History.ViolationID);
            return ProposedReturnValue;
        }
        public void JustUpdateHistory(int HistoryID,int PlateID, string PaidFine, string RegisteredOwner, string Address1, string State, int ViolationID)
        {

            _context.JustUpdateHistory(HistoryID,PlateID, PaidFine, RegisteredOwner, Address1, State, ViolationID);

        }
        public void JustUpdateHistory(HistoryBLL History)
        {

            _context.JustUpdateHistory(History.HistoryID,History.PlateID, History.PaidFine, History.RegisteredOwner, History.Address1, History.State, History.ViolationID);

        }

        #endregion HistoryBLL Stuff
        #region ObservationsBLL Stuff
        public ObservationsBLL FindObservation(int ObsID)
        {
            ObservationsBLL ProposedReturnValue = null;
            ObservationsDAL DataLayerObject = _context.FindObservation(ObsID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new ObservationsBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public List<ObservationsBLL> GetObservationsRelatedToDroneID(int DroneID, int skip, int take)
        {
            List<ObservationsBLL> ProposedReturnValue = new List<ObservationsBLL>();
            List<ObservationsDAL> ListOfDataLayerObjects = _context.GetObservationsRelatedToDroneID(DroneID, skip, take);
            foreach (ObservationsDAL Observations in ListOfDataLayerObjects)
            {
                ObservationsBLL BusinessObject = new ObservationsBLL(Observations);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public List<ObservationsBLL> GetObservationsRelatedToObs( int skip, int take)
        {
            List<ObservationsBLL> ProposedReturnValue = new List<ObservationsBLL>();
            List<ObservationsDAL> ListOfDataLayerObjects = _context.GetObservationsRelatedToObs( skip, take);
            foreach (ObservationsDAL Observations in ListOfDataLayerObjects)
            {
                ObservationsBLL BusinessObject = new ObservationsBLL(Observations);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public int ObtainObservationsCount()
        {
            int ProposedReturnValue = 0;
            ProposedReturnValue = _context.ObtainObservationsCount();
            return ProposedReturnValue;
        }
        public int CreateObservation(int Speed, string LatNumber, string LongNumber, int PlateID, string RegisteredOwner,int DroneID,string DroneName)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateObservation(Speed, LatNumber,LongNumber, PlateID,RegisteredOwner,DroneID);
            return ProposedReturnValue;
        }
        public int CreateObservation(ObservationsBLL Observations)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateObservation(Observations.Speed, Observations.LatNumber, Observations.LongNumber, Observations.PlateID, Observations.RegisteredOwner, Observations.DroneID);
            return ProposedReturnValue;
        }
        public void JustUpdateObservation(int ObsID,int Speed, string LatNumber, string LongNumber, int PlateID, string RegisteredOwner, int DroneID, string DroneName)
        {

            _context.JustUpdateObservation(ObsID,Speed, LatNumber, LongNumber, PlateID, RegisteredOwner, DroneID);

        }
        public void JustUpdateObservation(ObservationsBLL Observations)
        {

            _context.JustUpdateObservation(Observations.ObsID, Observations.Speed, Observations.LatNumber, Observations.LongNumber, Observations.PlateID, Observations.RegisteredOwner, Observations.DroneID);

        }
        public void DeleteObservation(int ObsID)
        {
            _context.DeleteObservation(ObsID);
        }
        public void DeleteObservation(ObservationsBLL Observations)
        {
            _context.DeleteObservation(Observations.ObsID);
        }

        #endregion ObservationsBLL Stuff
        #region UsersBLL Stuff
        public UsersBLL FindUser(int UserID)
        {
            UsersBLL ProposedReturnValue = null;
            UsersDAL DataLayerObject = _context.FindUser(UserID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new UsersBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public UsersBLL FindUserByEmail(string Email)
        {
            UsersBLL ProposedReturnValue = null;
            UsersDAL DataLayerObject = _context.FindUserByEmail(Email);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new UsersBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public List<UsersBLL> GetUsers( int skip, int take)
        {
            List<UsersBLL> ProposedReturnValue = new List<UsersBLL>();
            List<UsersDAL> ListOfDataLayerObjects = _context.GetUsers(skip, take);
            //This is looping over all of the DAL objects 
            foreach (UsersDAL Users in ListOfDataLayerObjects)
            {
                UsersBLL BusinessObject = new UsersBLL(Users);
                ProposedReturnValue.Add(BusinessObject);
                //Converting them one at a time into BLL objects and adding them
                //To the BLL List
            }
            return ProposedReturnValue;

        }
        public List<UsersBLL> GetUsersRelatedToRoleID(int RoleID,int Skip, int Take)
        {
            List<UsersBLL> ProposedReturnValue = new List<UsersBLL>();
            List<UsersDAL> ListOfDataLayerObjects = _context.GetUsersRelatedToRoleID(RoleID,Skip, Take);
            foreach (UsersDAL Users in ListOfDataLayerObjects)
            {
                UsersBLL BusinessObject = new UsersBLL(Users);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public int ObtainUserCount()
        {
            int ProposedReturnValue = 0;
            ProposedReturnValue = _context.ObtainUserCount();
            return ProposedReturnValue;
        }
        public int CreateUser(string Email, string UserName, string Hash, string Salt, int RoleID)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateUser( Email, UserName,Hash, Salt, RoleID);
            return ProposedReturnValue;
        }
        //This is the actually used create by MVC
        public int CreateUser(UsersBLL Users)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateUser(Users.Email,  Users.UserName, Users.Hash, Users.Salt, Users.RoleID);
            return ProposedReturnValue;
        }
        public void JustUpdateUser(int UserID,string Email, string UserName, string Hash, string Salt, int RoleID)
        {

            _context.JustUpdateUser( UserID,Email, UserName, Hash, Salt, RoleID);

        }
        public void JustUpdateUser(UsersBLL Users)
        {

            _context.JustUpdateUser(Users.UserID,Users.Email, Users.UserName, Users.Hash, Users.Salt, Users.RoleID);

        }
        public void DeleteUser(int UserID)
        {
            _context.DeleteUser(UserID);
        }
        public void DeleteUser(UsersBLL Users)
        {
            _context.DeleteUser(Users.UserID);
        }


        #endregion UserBLL Stuff
        #region ViolationsBLL Stuff
        public ViolationsBLL FindViolationByViolationID(int ViolationID)
        {
            ViolationsBLL ProposedReturnValue = null;
            ViolationsDAL DataLayerObject = _context.FindViolationByViolationID(ViolationID);
            if (null != DataLayerObject)
            {
                ProposedReturnValue = new ViolationsBLL(DataLayerObject);
            }
            return ProposedReturnValue;
        }
        public List<ViolationsBLL> GetViolations(int Skip, int Take)
        {
            List<ViolationsBLL> ProposedReturnValue = new List<ViolationsBLL>();
            List<ViolationsDAL> ListOfDataLayerObjects = _context.GetViolations(Skip, Take);
            foreach (ViolationsDAL Violations in ListOfDataLayerObjects)
            {
                ViolationsBLL BusinessObject = new ViolationsBLL(Violations);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public List<ViolationsBLL> GetViolationsRelatedToObsID(int ObsID, int Skip, int Take)
        {
            List<ViolationsBLL> ProposedReturnValue = new List<ViolationsBLL>();
            List<ViolationsDAL> ListOfDataLayerObjects = _context.GetViolationsRelatedToObsID(ObsID, Skip, Take);
            foreach (ViolationsDAL Violations in ListOfDataLayerObjects)
            {
                ViolationsBLL BusinessObject = new ViolationsBLL(Violations);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }
        public int ObtainViolationsCount()
        {
            int ProposedReturnValue = 0;
            ProposedReturnValue = _context.ObtainViolationsCount();
            return ProposedReturnValue;
        }
        public int CreateViolation(string ViolationDesc, int RecordSpeed, Decimal FineAmount,int PlateID,int ObsID)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateViolation(ViolationDesc, RecordSpeed, FineAmount, PlateID, ObsID);
            return ProposedReturnValue;
        }
        public int CreateViolation(ViolationsBLL Violations)
        {
            int ProposedReturnValue = -1;
            ProposedReturnValue = _context.CreateViolation(Violations.ViolationDesc, Violations.RecordSpeed, Violations.FineAmount, Violations.PlateID, Violations.ObsID);
            return ProposedReturnValue;
        }
        public void JustUpdateViolation(int ViolationID,string ViolationDesc, int RecordSpeed, Decimal FineAmount, int PlateID, int ObsID)
        {

            _context.JustUpdateViolation(ViolationID,ViolationDesc, RecordSpeed, FineAmount, PlateID, ObsID);

        }
        public void JustUpdateViolation(ViolationsBLL Violations)
        {

            _context.JustUpdateViolation(Violations.ViolationID,Violations.ViolationDesc, Violations.RecordSpeed, Violations.FineAmount, Violations.PlateID, Violations.ObsID);

        }
        public void ViolationDelete(int ViolationID)
        {
            _context.ViolationDelete(ViolationID);
        }
        public void ViolationDelete(ViolationsBLL Violations)
        {
            _context.ViolationDelete(Violations.ViolationID);
        }


        #endregion ViolationsBLL Stuff

    }
}    
