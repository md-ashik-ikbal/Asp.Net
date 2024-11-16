using CRUD.DTOS;
using CRUD.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        readonly CRUDEntities db = new CRUDEntities();

        // converts entity of Student to CreateStudentDTO
        public static CreateStudentDTO Convert(Student s)
        {
            return new CreateStudentDTO()
            {
                Name = s.Name,
                Cgpa = s.Cgpa,
            };
        }

        // conterts CreateStudentDTO to entity of Student
        public static Student Convert(CreateStudentDTO s)
        {
            return new Student()
            {
                Name = s.Name,
                Cgpa = s.Cgpa,
            };
        }

        // converts List of entity of Student into List of CreateStudentDTO
        public List<CreateStudentDTO> Convert(List<Student> s)
        {
            var list = new List<CreateStudentDTO>();
            foreach(var i in s)
            {
                list.Add(Convert(i));
            }

            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateStudentDTO());
        }

        [HttpPost]
        public ActionResult Create(CreateStudentDTO student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(Convert(student));
                db.SaveChanges();

                return RedirectToAction("AllStudents");
            }
            else
            {
                return View(student);
            }
        }

        [HttpGet]
        public ActionResult AllStudents()
        {
            return View(db.Students.ToList());
            //return View(Convert(db.Students.ToList()));
        }

        public ActionResult StudentById(int id)
        {
            return View(db.Students.Find(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Students.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, CreateStudentDTO s)
        {
            if (ModelState.IsValid)
            {
                var getStudent = db.Students.Find(id);

                if (getStudent == null)
                {
                    TempData["unf"] = "User Not Found!";

                    return View();
                }
                else
                {
                    getStudent.Name = s.Name;
                    getStudent.Cgpa = s.Cgpa;
                    db.SaveChanges();

                    return RedirectToAction("AllStudents");
                }
            }
            else
            {
                return View(s);
            }
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            return View(db.Students.Find(id));
        }

        [HttpPost]
        public ActionResult Remove(int Id, string decision)
        {
            var student = db.Students.Find(Id);

            if(decision == "Yes")
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }

            return RedirectToAction("AllStudents");
        }
    }
}