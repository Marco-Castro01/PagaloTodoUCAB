﻿namespace UCABPagaloTodoMS.Application.Requests
{
    public class AdminRequest
    {
        public string? email { get; set; } 
        public string? password { get; set; }
        public string? nickName { get; set; }
        public bool status { get; set; }
        public string? cedula { get; set; } 
    }
}
