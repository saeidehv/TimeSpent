
(function(ts){
    var AccountRegisterModelStep1 = function() {

        var self = this;

        self.FirstName = '';
        self.LastName = '';
        self.Address = '';
      //  self.City = '';
     //   self.State = '';
     //   self.ZipCode = '';
    
        self.Initialized = false;
    }
    ts.AccountRegisterModelStep1 = AccountRegisterModelStep1;

}(window.TimeSpent));


(function (ts) {
    var AccountRegisterModelStep2 = function () {

        var self = this;

        self.LoginEmail = '';
        self.Password = '';
        self.PasswordConfirm = '';

        self.Initialized = false;
    }
    ts.AccountRegisterModelStep2 = AccountRegisterModelStep2;
}(window.TimeSpent));