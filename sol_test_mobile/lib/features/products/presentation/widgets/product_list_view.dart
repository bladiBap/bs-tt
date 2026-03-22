import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_list_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_list_state.dart';
import 'package:sol_test_mobile/features/products/presentation/widgets/product_list_item.dart';

class ProductListView extends StatefulWidget {
  const ProductListView({super.key});

  @override
  State<ProductListView> createState() => _ProductListViewState();
}

class _ProductListViewState extends State<ProductListView> {
  final _searchController = TextEditingController();

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  void _onSearch() {
    context.read<ProductListCubit>().fetchProducts(query: _searchController.text);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("List of Products"),
        centerTitle: true,
      ),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(12.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _searchController,
                    decoration: InputDecoration(
                      hintText: "Search products...",
                      prefixIcon: const Icon(Icons.search),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                      suffixIcon: IconButton(
                        icon: const Icon(Icons.clear),
                        onPressed: () {
                          _searchController.clear();
                          _onSearch();
                        },
                      ),
                    ),
                    onSubmitted: (_) => _onSearch(),
                  ),
                ),
                const SizedBox(width: 8),
                ElevatedButton(
                  onPressed: _onSearch,
                  style: ElevatedButton.styleFrom(
                    shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
                    padding: const EdgeInsets.symmetric(vertical: 16, horizontal: 16),
                  ),
                  child: const Icon(Icons.arrow_forward),
                ),
              ],
            ),
          ),
          Expanded(
            child: BlocBuilder<ProductListCubit, ProductListState>(
              builder: (context, state) {
                return switch (state) {
                  ProductListInitial() || ProductListLoading() => 
                    const Center(child: CircularProgressIndicator()),
                  
                  ProductListError(message: var msg) => 
                    Center(child: Text(msg, style: const TextStyle(color: Colors.red))),
                  
                  ProductListLoaded(products: var list) => list.isEmpty 
                    ? const Center(child: Text("No products found"))
                    : ListView.separated(
                        padding: const EdgeInsets.all(8),
                        itemCount: list.length,
                        separatorBuilder: (context, index) => const Divider(),
                        itemBuilder: (context, index) {
                          final product = list[index];
                          return ProductListItem(product: product);
                        },
                      ),
                };
              },
            ),
          ),
        ],
      ),
    );
  }
}
