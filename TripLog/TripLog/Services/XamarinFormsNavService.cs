using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Services
{
	public class XamarinFormsNavService : INavService
	{

		public INavigation NavigationInstance { get; set; }

		/// <summary>
		/// Page to ViewModel mapping
		/// </summary>
		private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

		public void RegisterViewMapping(Type viewModel, Type view)
		{
			_map.Add(viewModel, view);
		}

		public bool CanGoBack
		{
			get
			{
				return NavigationInstance.NavigationStack != null && NavigationInstance.NavigationStack.Count > 0;
			}
		}

		public event PropertyChangedEventHandler CanGoBackChanged;

		private void onCanGoBackChanged()
		{
			var handler = CanGoBackChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs("CanGoBack"));
		}

		public Task ClearBackStack()
		{
			if (NavigationInstance.NavigationStack.Count <= 1)
				return Task.FromResult(0);
			for (var i = 0; i < NavigationInstance.NavigationStack.Count - 1; i++)
			{
				NavigationInstance.RemovePage(NavigationInstance.NavigationStack[i]);
			}
			return Task.FromResult(0);
		}

		public async Task GoBack()
		{
			if (CanGoBack)
				await NavigationInstance.PopAsync(animated: true);
			onCanGoBackChanged();
		}

		public async Task NavigateTo<TVM>() where TVM : BaseViewModel
		{
			await navigateToView(typeof(TVM));
			var baseViewModel = NavigationInstance.NavigationStack.Last().BindingContext as BaseViewModel;
			if (baseViewModel != null)
				await baseViewModel.Init();
		}

		public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
		{
			await navigateToView(typeof(TVM));
			var baseViewModel = NavigationInstance.NavigationStack.Last().BindingContext as BaseViewModel<TParameter>;
			if (baseViewModel != null)
				await baseViewModel.Init(parameter);
		}

		public Task NavigateToUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentException("Invalid uri");
			Device.OpenUri(uri);
			return Task.FromResult(0);
		}

		public Task RemoveLastView()
		{
			if (NavigationInstance.NavigationStack.Any())
			{
				var lastView = NavigationInstance.NavigationStack.Last();
				NavigationInstance.RemovePage(lastView);
			}
			return Task.FromResult(0);
		}

		private async Task navigateToView(Type viewModelType)
		{
			Type viewType;
			if (!_map.TryGetValue(viewModelType, out viewType))
				throw new ArgumentException($"No view found in View Mapping for '{nameof(viewModelType)}'");
			var constructor = viewType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(dc => dc.GetParameters().Count() <= 0);
			var view = constructor.Invoke(null) as Page;
			var vm = ((App)Application.Current).Kernel.GetService(viewModelType);
			view.BindingContext = vm;
			await NavigationInstance.PushAsync(view, animated: true);
		}
	}
}
