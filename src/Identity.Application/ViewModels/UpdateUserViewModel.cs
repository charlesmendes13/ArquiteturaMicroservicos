﻿namespace Identity.Application.ViewModels
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
