using System.Diagnostics;
using System.Xml.Linq;

namespace Zenith_MAUI.Components;

public partial class PlaylistComponent : ContentView
{
    public static BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(PlaylistComponent), "/", BindingMode.TwoWay);

    public static BindableProperty IdProperty =
        BindableProperty.Create(nameof(Id), typeof(int), typeof(PlaylistComponent), 0, BindingMode.OneWay);

    public static BindableProperty CreatedAtProperty =
        BindableProperty.Create(nameof(CreatedAt), typeof(DateTime), typeof(PlaylistComponent), DateTime.MinValue, BindingMode.OneWay);

    public static BindableProperty TracksCountProperty =
       BindableProperty.Create(nameof(TracksCount), typeof(int), typeof(PlaylistComponent), 0, BindingMode.OneWay);



    public PlaylistComponent()
	{
		InitializeComponent();
	}

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public int Id
    {
        get => (int)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public DateTime CreatedAt
    {
        get => (DateTime)GetValue(CreatedAtProperty);
        set => SetValue(CreatedAtProperty, value);
    }

    public int TracksCount
    {
        get => (int)GetValue(TracksCountProperty);
        set => SetValue(TracksCountProperty, value);
    }

}