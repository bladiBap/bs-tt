import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/domain/entities/product.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_detail_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_list_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_update_price_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/pages/product_update_price_page.dart';
import 'package:sol_test_mobile/features/service_locator.dart' as sl;

class ProductListItem extends StatelessWidget {
  final Product product;

  const ProductListItem({super.key, required this.product});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      leading: const Icon(Icons.shopping_bag, color: Colors.blue),
      title: Text(product.name, style: const TextStyle(fontWeight: FontWeight.bold)),
      subtitle: Text("SKU: ${product.sku}"),
      trailing: Text(
        "${product.currency?.symbol ?? ''} ${product.price}",
        style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
      ),
      onTap: () async {
        final bool? refresh = await Navigator.push(
          context,
          MaterialPageRoute(
            builder: (_) => MultiBlocProvider(
              providers: [
                BlocProvider(create: (_) => sl.sl<ProductDetailCubit>()),
                BlocProvider(create: (_) => sl.sl<ProducUpdatePriceCubit>()),
              ],
              child: ProductEditPricePage(productId: product.id),
            ),
          ),
        );
        if (refresh == true && context.mounted) {
          context.read<ProductListCubit>().fetchProducts();
        }
      },
    );
  }
}
