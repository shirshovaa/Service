﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Service
{
    class Database
    {
        private SQLiteConnection dbConnect = new SQLiteConnection("Data Source=\"service_db.db\"");
        private SQLiteCommand sqlCmd;
        private String sqlQuery;
        #region Получить всех ремонтников
        public List<Repairer> SelectRepairers()
        {
            List<Repairer> repairers = new List<Repairer>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM repairer";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                repairers.Add(new Repairer(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][1].ToString(), table.Rows[i][2].ToString(), table.Rows[i][3].ToString(), table.Rows[i][4].ToString()));
            }
            dbConnect.Close();
            return repairers;
        }
        #endregion
        #region Получить все статусы
        public List<Status> SelectStatuses()
        {
            List<Status> statuses = new List<Status>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM statuses";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                statuses.Add(new Status(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][1].ToString()));
            }
            dbConnect.Close();
            return statuses;
        }
        #endregion
        #region Получить все отделы
        public List<Dept> SelectDepts()
        {
            List<Dept> depts = new List<Dept>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM dept";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                depts.Add(new Dept(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][1].ToString(), table.Rows[i][2].ToString(), table.Rows[i][3].ToString()));
            }
            dbConnect.Close();
            return depts;
        }
        #endregion
        #region Получить все типы
        public List<TypeModel> SelectTypesModel()
        {
            List<TypeModel> typesModel = new List<TypeModel>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM 'types'";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                typesModel.Add(new TypeModel(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][1].ToString(), table.Rows[i][2].ToString()));
            }
            dbConnect.Close();
            return typesModel;
        }
        #endregion
        #region Получить все модели
        public List<Model> SelectModels(List<TypeModel> typeModels)
        {
            List<Model> models = new List<Model>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM model";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (TypeModel typeModel in typeModels)
                {
                    if (int.Parse(table.Rows[i][1].ToString()) == typeModel.RowId)
                        models.Add(new Model(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][2].ToString(), table.Rows[i][3].ToString(), typeModel));
                }
            }
            dbConnect.Close();
            return models;
        }
        #endregion
        #region Получить все параметры
        public List<Parameter> SelectParameters(List<Model> models)
        {
            List<Parameter> parameters = new List<Parameter>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM parameters";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Model model in models)
                {
                    if (int.Parse(table.Rows[i][1].ToString()) == model.RowId)
                        parameters.Add(new Parameter(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][2].ToString(), table.Rows[i][3].ToString(), model, table.Rows[i][4].ToString()));
                }
            }
            dbConnect.Close();
            return parameters;
        }
        #endregion        
        #region Получить все виды работы
        public List<Service> SelectService(List<Model> models)
        {
            List<Service> services = new List<Service>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM service";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Model model in models)
                {
                    if (int.Parse(table.Rows[i][1].ToString()) == model.RowId)
                        services.Add(new Service(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][2].ToString(), table.Rows[i][3].ToString(), table.Rows[i][4].ToString(), model));
                }
            }
            dbConnect.Close();
            return services;
        }
        #endregion
        #region Получить все запчасти
        public List<Spares> SelectSpares(List<Model> models)
        {
            List<Spares> spares = new List<Spares>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM spares";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Model model in models)
                {
                    if (int.Parse(table.Rows[i][1].ToString()) == model.RowId)
                        spares.Add(new Spares(int.Parse(table.Rows[i][0].ToString()), table.Rows[i][2].ToString(), table.Rows[i][3].ToString(), model));
                }
            }
            dbConnect.Close();
            return spares;
        }
        #endregion
        #region Получить все устройства
        public List<Device> SelectDevices(List<Model> models, List<Dept> depts, List<Status> statuses)
        {
            List<Device> devices = new List<Device>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM devices";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Model model in models)
                {
                    foreach (Dept dept in depts)
                    {
                        foreach (Status status in statuses)
                        {
                            if ((int.Parse(table.Rows[i][1].ToString()) == model.RowId) && (int.Parse(table.Rows[i][2].ToString()) == dept.RowId) && (int.Parse(table.Rows[i][3].ToString()) == status.RowId))
                                devices.Add(new Device(int.Parse(table.Rows[i][0].ToString()), model, dept, status, table.Rows[i][4].ToString(), table.Rows[i][5].ToString()));
                        }
                    }
                }
            }
            dbConnect.Close();
            return devices;
        }
        #endregion
        #region Получить все записи из журнала ремонтов
        public List<ServiceLog> SelectServiceLog(List<Device> devices, List<Repairer> repairers)
        {
            List<ServiceLog> serviceLogs = new List<ServiceLog>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM serviceLog";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Device device in devices)
                {
                    foreach (Repairer repairer in repairers)
                    {
                        if ((int.Parse(table.Rows[i][1].ToString()) == device.RowId) && (int.Parse(table.Rows[i][2].ToString()) == repairer.RowId))
                            serviceLogs.Add(new ServiceLog(int.Parse(table.Rows[i][0].ToString()), device, new Date(table.Rows[i][3].ToString()), repairer));
                    }
                }
            }
            dbConnect.Close();
            return serviceLogs;
        }
        #endregion
        #region Получить все значения параметров
        public List<ParametersValues> SelectParametersValues(List<Parameter> parameters, List<ServiceLog> serviceLogs)
        {
            List<ParametersValues> parametersValues = new List<ParametersValues>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM parametersValues";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Parameter parameter in parameters)
                {
                    foreach (ServiceLog serviceLog in serviceLogs)
                    {
                        if ((int.Parse(table.Rows[i][1].ToString()) == parameter.RowId) && (int.Parse(table.Rows[i][2].ToString()) == serviceLog.RowId))
                            parametersValues.Add(new ParametersValues(int.Parse(table.Rows[i][0].ToString()), parameter, serviceLog, table.Rows[i][3].ToString()));
                    }
                }
            }
            dbConnect.Close();
            return parametersValues;
        }
        #endregion
        #region Получить все проделанные работы
        public List<ServiceDone> SelectServicesDone(List<Service> services, List<ServiceLog> serviceLogs)
        {
            List<ServiceDone> servicesDone = new List<ServiceDone>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM serviceDone";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Service service in services)
                {
                    foreach (ServiceLog serviceLog in serviceLogs)
                    {
                        if ((int.Parse(table.Rows[i][1].ToString()) == service.RowId) && (int.Parse(table.Rows[i][2].ToString()) == serviceLog.RowId))
                            servicesDone.Add(new ServiceDone(int.Parse(table.Rows[i][0].ToString()), service, serviceLog));
                    }
                }
            }
            dbConnect.Close();
            return servicesDone;
        }
        #endregion
        #region Получить все использованные запчасти
        public List<SparesUsed> SelectSparesUsed(List<Spares> spares, List<ServiceLog> serviceLogs)
        {
            List<SparesUsed> sparesUsed = new List<SparesUsed>();
            DataTable table = new DataTable();
            sqlQuery = "Select * FROM sparesUsed";
            dbConnect.Open();
            if (dbConnect.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с базой данных!");
                return null;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnect);
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (Spares spare in spares)
                {
                    foreach (ServiceLog serviceLog in serviceLogs)
                    {
                        if ((int.Parse(table.Rows[i][1].ToString()) == spare.RowId) && (int.Parse(table.Rows[i][2].ToString()) == serviceLog.RowId))
                            sparesUsed.Add(new SparesUsed(int.Parse(table.Rows[i][0].ToString()), spare, serviceLog));
                    }
                }
            }
            dbConnect.Close();
            return sparesUsed;
        }
        #endregion
    }
}