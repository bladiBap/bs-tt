import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_detail_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_update_price_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_detail_state.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_update_price_state.dart';

class ProductEditPricePage extends StatefulWidget {
    final String productId;

    const ProductEditPricePage({super.key, required this.productId});

    @override
    State<ProductEditPricePage> createState() => _ProductEditPricePageState();
}

class _ProductEditPricePageState extends State<ProductEditPricePage> {
    final _priceController = TextEditingController();
    final _formKey = GlobalKey<FormState>();

    @override
    void initState() {
        super.initState();
        context.read<ProductDetailCubit>().loadProduct(widget.productId);
    }

    @override
    void dispose() {
        _priceController.dispose();
        super.dispose();
    }

    @override
    Widget build(BuildContext context) {
        return Scaffold(
            appBar: AppBar(title: const Text("Edit Product Price")),
            body: MultiBlocListener(
                listeners: [
                    BlocListener<ProductDetailCubit, ProductDetailState>(
                        listener: (context, state) {
                            if (state is ProductDetailLoaded) {
                                _priceController.text = state.price.toString();
                            }
                        },
                    ),
                    BlocListener<ProducUpdatePriceCubit, UpdatePriceProductState>(
                        listener: (context, state) {
                            if (state is UpdatePriceProductSuccess) {
                                ScaffoldMessenger.of(context).showSnackBar(
                                    const SnackBar(content: Text("Price updated successfully")),
                                );
                                Navigator.pop(context, true);
                            }
                            if (state is UpdatePriceProductError) {
                                ScaffoldMessenger.of(context).showSnackBar(
                                    SnackBar(content: Text(state.message), backgroundColor: Colors.red),
                                );
                            }
                        },
                    ),
                ],
                child: BlocBuilder<ProductDetailCubit, ProductDetailState>(
                    builder: (context, state) {
                        if (state is ProductDetailLoading) {
                            return const Center(child: CircularProgressIndicator());
                        }

                        if (state is ProductDetailError) {
                            return Center(child: Text(state.message));
                        }

                        if (state is ProductDetailLoaded) {
                            return SingleChildScrollView(
                                padding: const EdgeInsets.all(16.0),
                                child: Column(
                                    children: [
                                        Card(
                                            elevation: 3,
                                            child: ListTile(
                                                title: Text(state.name, style: const TextStyle(fontWeight: FontWeight.bold)),
                                                subtitle: Text("SKU: ${state.sku}"),
                                                trailing: Text(state.currencySymbol, style: const TextStyle(fontSize: 18)),
                                                leading: const Icon(Icons.inventory_2),
                                            ),
                                        ),
                                        const SizedBox(height: 24),
                                        Form(
                                            key: _formKey,
                                            child: TextFormField(
                                                controller: _priceController,
                                                decoration: const InputDecoration(
                                                    labelText: "New Price",
                                                    border: OutlineInputBorder(),
                                                    prefixIcon: Icon(Icons.edit_note),
                                                ),
                                                keyboardType: const TextInputType.numberWithOptions(decimal: true),
                                                validator: (v) {
                                                    if (v == null || v.isEmpty) return "Field is required";
                                                    if (double.tryParse(v) == null) return "Must be a valid number";
                                                    if (double.parse(v) < 0) return "The price cannot be negative";
                                                    return null;
                                                },
                                            ),
                                        ),
                                        const SizedBox(height: 32),
                                        
                                        BlocBuilder<ProducUpdatePriceCubit, UpdatePriceProductState>(
                                            builder: (context, updateState) {
                                                return SizedBox(
                                                    width: double.infinity,
                                                    height: 50,
                                                    child: ElevatedButton(
                                                        onPressed: updateState is UpdatePriceProductLoading
                                                            ? null
                                                            : () {
                                                                if (_formKey.currentState!.validate()) {
                                                                    context.read<ProducUpdatePriceCubit>().updatePrice(
                                                                        productId: widget.productId,
                                                                        newPrice: double.parse(_priceController.text),
                                                                    );
                                                                }
                                                            },
                                                        child: updateState is UpdatePriceProductLoading
                                                            ? const CircularProgressIndicator(color: Colors.white)
                                                            : const Text("ACTUALIZAR PRECIO"),
                                                    ),
                                                );
                                            },
                                        ),
                                    ],
                                ),
                            );
                        }
                        return const SizedBox();
                    },
                ),
            ),
        );
    }
}