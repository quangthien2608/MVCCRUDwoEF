using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCCRUDwoEF.Data;
using MVCCRUDwoEF.Models;

namespace MVCCRUDwoEF.Controllers
{
    public class PenController : Controller
    {
        private readonly IConfiguration _configuration;

        public PenController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Pen
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("PenViewAll", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
        }

        // GET: Pen/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Pen/AddOrEdit/
        public IActionResult AddOrEdit(int? id)
        {
            PenViewModel penViewModel = new PenViewModel();
            if (id > 0)
                penViewModel = FetchBookByID(id);
            return View(penViewModel);
        }

        // POST: Pen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("PenID,Title,Type,Price")] PenViewModel penViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("PenAddOrEdit", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("PenID", penViewModel.PenID);
                    sqlCmd.Parameters.AddWithValue("Title", penViewModel.Title);
                    sqlCmd.Parameters.AddWithValue("Type", penViewModel.Type);
                    sqlCmd.Parameters.AddWithValue("Price", penViewModel.Price);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(penViewModel);
        }

        // GET: Pen/Delete/5
        public IActionResult Delete(int? id)
        {
            PenViewModel penViewModel = FetchBookByID(id);
            return View(penViewModel);
        }

        // POST: Pen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("PenDeleteByID", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("PenID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public PenViewModel FetchBookByID(int? id)
        {
            PenViewModel penViewModel = new PenViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("PenViewByID", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("PenID", id);
                sqlDa.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    penViewModel.PenID = Convert.ToInt32(dtbl.Rows[0]["PenID"].ToString());
                    penViewModel.Title = dtbl.Rows[0]["Title"].ToString();
                    penViewModel.Type = dtbl.Rows[0]["Type"].ToString();
                    penViewModel.Price = Convert.ToInt32(dtbl.Rows[0]["Price"].ToString());
                }
                return penViewModel;
            }
        }
    }
}
