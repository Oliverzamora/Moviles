namespace MauizAppUtn;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}
	private void Option1_Clicked(object sender, EventArgs e)
	{
        Navigation.PushAsync(new NewPage2());

    }
    private void Option2_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewPage1());

    }
}