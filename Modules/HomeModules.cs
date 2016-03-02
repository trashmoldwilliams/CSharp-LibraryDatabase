using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Library
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      // Get["/student"] = _ => {
      //   List<Student> AllStudent = Student.GetAll();
      //   return View["students.cshtml", AllStudent];
      // };
      //
      // Post["/students/new"] = _ => {
      //   var newStudent = new Student(Request.Form["student-name"], Request.Form["student-date"]);
      //   newStudent.Save();
      //   List<Student> AllStudent = Student.GetAll();
      //   return View["students.cshtml", AllStudent];
      // };
      //
      // Get["/student/{id}"] = parameters => {
      //   var model = new Dictionary<string, object>();
      //   var selectedStudent = Student.Find(parameters.id);
      //   List<Course> Studentcourse = selectedStudent.GetCourses();
      //   var AllStudents = Student.GetAll();
      //   model.Add("student", selectedStudent);
      //   model.Add("courses", Studentcourse);
      //   model.Add("students", AllStudents);
      //   return View["courses.cshtml", model];
      // };
      //
      // Post["/students/{id}/new"] = parameters => {
      //   var newCourse = new Course(Request.Form["name"], Request.Form["student-id"]);
      //   newCourse.Save();
      //
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Student SelectedStudent = Student.Find(parameters.id);
      //
      //   SelectedStudent.AddCourses(newCourse);
      //
      //   List<Course> StudentCourse = SelectedStudent.GetCourses();
      //   List<Student> AllStudents = Student.GetAll();
      //   model.Add("student", SelectedStudent);
      //   model.Add("courses", StudentCourse);
      //   model.Add("students", AllStudents);
      //   return View["courses.cshtml", model];
      // };
      //
      // Get["/course/edit/{id}"] = parameters => {
      //   Student SelectedStudent = Student.Find(parameters.id);
      //   return View["student_edit.cshtml", SelectedStudent];
      // };

      // Patch["/stylist/edit/{id}"] = parameters => {
      //   Stylist SelectedStylist = Stylist.Find(parameters.id);
      //   SelectedStylist.Update(Request.Form["stylist-name"]);
      //
      //   List<Stylist> AllStylists = Stylist.GetAll();
      //   return View["stylists.cshtml", AllStylists];
      // };
      //
      // Delete["/stylist/delete/{id}"] = parameters => {
      //   Stylist SelectedStylist = Stylist.Find(parameters.id);
      //   SelectedStylist.Delete();
      //
      //   List<Stylist> AllStylists = Stylist.GetAll();
      //   return View["stylists.cshtml", AllStylists];
      // };
      //
      // Get["/client/edit/{id}"] = parameters => {
      //   Client SelectedClient = Client.Find(parameters.id);
      //   return View["client_edit.cshtml", SelectedClient];
      // };
      //
      // Patch["/client/edit/{id}"] = parameters => {
      //   Client SelectedClient = Client.Find(parameters.id);
      //   SelectedClient.Update(Request.Form["client-name"]);
      //
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
      //   List<Client> StylistClient = SelectedStylist.GetClients();
      //   List<Stylist> AllStylists = Stylist.GetAll();
      //   model.Add("stylist", SelectedStylist);
      //   model.Add("clients", StylistClient);
      //   model.Add("stylists", AllStylists);
      //   return View["clients.cshtml", model];
      // };
      //
      // Delete["/client/delete/{id}"] = parameters => {
      //   Client SelectedClient = Client.Find(parameters.id);
      //   SelectedClient.Delete();
      //
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
      //   List<Client> StylistClient = SelectedStylist.GetClients();
      //   List<Stylist> AllStylists = Stylist.GetAll();
      //   model.Add("stylist", SelectedStylist);
      //   model.Add("clients", StylistClient);
      //   model.Add("stylists", AllStylists);
      //   return View["clients.cshtml", model];
      // };
    }
  }
}
