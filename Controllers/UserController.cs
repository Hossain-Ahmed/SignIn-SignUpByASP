﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USAPolice.Models;
using USAPolice.ViewModels;
using USAPolice.Repositories;

namespace USAPolice.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        private ProjectDbContext db = new ProjectDbContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
            var user = new User
            {
                Id = 2,
                Name = userViewModel.Name,
                Password = userViewModel.Password,
                Email = userViewModel.Email
            };
            var confirmPassword = userViewModel.RepeatPassword;

            if (confirmPassword != userViewModel.Password)
            {
                return RedirectToAction("Login", "User");
            }

            user.RepeatPassword = confirmPassword;

           var a = user;

            db.Users.Add(user);
            db.SaveChanges();


            //var isSaved = _userRepository.SaveUser(user);

            //if (isSaved)
            //{
            //    return RedirectToAction("Login", "User");
            //}
            //else
            //{
            //    ViewBag.ValidationMessage = "Username or Password is not correct!";

            //    return View();
            //}
            return RedirectToAction("Login", "User");

        }
    
        [HttpGet]
        public ActionResult Login(UserViewModel userViewModel)
        {
            var user = new User
            {
                Name = userViewModel.Name,
                Password = userViewModel.Password
            };
            var isExist = db.Users.Any(x => x.Name.Equals(user.Name)
            && x.Password.Equals(user.Password));
            var userList = db.Users.ToList();
            // var isExist = _userRepository.IsUserValid(user);



            if (isExist)
            {
                return RedirectToAction("index", "User");
            }
            else
            {
                ViewBag.ValidationMessage = "Username or Password is not correct!";

                return View();
            }
        }

    }
}