(function(ts){
    var AddEntryModel = function () {

        var self = this;
        self.TimeEntryId = '';
        self.Duration ='';
        self.Date = '';
        self.User = '';
        self.Project = '';
        self.Category = '';
        self.Description = '';
        
    }
    ts.AddEntryModel = AddEntryModel;
}(window.TimeSpent));

