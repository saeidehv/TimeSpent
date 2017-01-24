(function(ts){
    var TimeEntryModel = function () {

        var self = this;
        self.TimeEntryId ='';
        self.Duration ='';
        self.Date = '';
        self.User = '';
        self.ProjectId = '';
        self.CategoryId = '';
        self.Description = '';
        
    }
    ts.TimeEntryModel = TimeEntryModel;
}(window.TimeSpent));