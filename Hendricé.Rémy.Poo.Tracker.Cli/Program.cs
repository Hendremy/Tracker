﻿using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        private readonly ITrackerRepository _repository;
        private readonly IAuthenticate _authenticator;

        private Program()
        {
            var repository = new JSONTrackerRepository("../../../../../json", "users.json", "plannings");
            var authenticator = new Authenticator();
            var mainSuperviser = CreateMainSuperviser(repository);
            CreateAuthentifySuperviser(mainSuperviser, repository, authenticator);
        }

        private void CreateAuthentifySuperviser(MainSuperviser mainSuperviser, ITrackerRepository repo, IAuthenticate auth)
        {
            var view = new AuthenticateView();
            var authSuperviser = new AuthenticateSuperviser(view, repo, auth);
            view.ShowDialog();
            authSuperviser.UserAuthentified += mainSuperviser.OnUserAuthentified;
        }

        private MainSuperviser CreateMainSuperviser(ITrackerRepository repo)
        {
            var view = new MainView();
            var sorter = initSortHandler();
            var filter = initFilterHandler();
            var mainSuperviser = new MainSuperviser(view, repo, sorter, filter);
            return mainSuperviser;
        }

        private SortHandler initSortHandler()
        {
            var startdate = new StartDateSort(null);
            var status = new StatusSort(startdate);
            var planningsort = new PlanningSort(status);
            return new SortHandler(planningsort);
        }

        private FilterHandler initFilterHandler()
        {
            var date = new DateFilter(null);
            var status = new StatusFilter(date);
            var planning = new PlanningFilter(status);
            return new FilterHandler(planning);
        }
    }
}
