using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace Tools.Md5.WPF.Behaviors;

public class SelectedItemsBehavior : Behavior<MultiSelector>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.SelectionChanged += OnSelectedChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        AssociatedObject.SelectionChanged -= OnSelectedChanged;
    }

    private void OnSelectedChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (sender is not MultiSelector s) return;

        BindableSelectedItems = s.SelectedItems;

        s.GetBindingExpression(BindableSelectedItemsProperty)?.UpdateSource();
    }


    public static readonly DependencyProperty BindableSelectedItemsProperty = DependencyProperty.Register(
        nameof(BindableSelectedItems), typeof(IList), typeof(SelectedItemsBehavior), new PropertyMetadata(null));

    public IList BindableSelectedItems
    {
        get => (IList)GetValue(BindableSelectedItemsProperty);
        set => SetValue(BindableSelectedItemsProperty, value);
    }
}