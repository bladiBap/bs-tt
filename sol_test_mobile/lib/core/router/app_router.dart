import 'package:flutter/material.dart';
import 'package:sol_test_mobile/features/products/presentation/pages/product_list_page.dart';

class AppRouter {
  static const String productList = '/';
  static const String productDetail = '/product_detail';
  static const String productCreate = '/product_create';
  static const String productEditPrice = '/product_edit_price';

  static Route<dynamic> generateRoute(RouteSettings settings) {
    switch (settings.name) {
      case productList:
        return MaterialPageRoute(builder: (_) => const ProductListPage());
      
      // case productDetail:
      //   final productId = settings.arguments as String;
      //   return MaterialPageRoute(builder: (_) => ProductDetailPage(id: productId));

      default:
        return MaterialPageRoute(
          builder: (_) => Scaffold(
            body: Center(child: Text('Error: ${settings.name}')),
          ),
        );
    }
  }
}