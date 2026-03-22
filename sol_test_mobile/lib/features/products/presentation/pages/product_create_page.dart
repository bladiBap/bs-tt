import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_create_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_create_state.dart';

class ProductCreatePage extends StatefulWidget {
    const ProductCreatePage({super.key});

    @override
    State<ProductCreatePage> createState() => _ProductCreatePageState();
}

class _ProductCreatePageState extends State<ProductCreatePage> {
    final _formKey = GlobalKey<FormState>();
    final _nameController = TextEditingController();
    final _priceController = TextEditingController();
    final _currencyController = TextEditingController();

    @override
    void dispose() {
        _nameController.dispose();
        _priceController.dispose();
        _currencyController.dispose();
        super.dispose();
    }

    @override
    Widget build(BuildContext context) {
        return Scaffold(
            appBar: AppBar(title: const Text("New Product")),
            body: BlocConsumer<CreateProductCubit, CreateProductState>(
                listener: (context, state) {
                    if (state is CreateProductSuccess) {
                        ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(content: Text("Product created successfully")),
                        );
                        Navigator.pop(context, true);
                    }
                    if (state is CreateProductError) {
                        ScaffoldMessenger.of(context).showSnackBar(
                            SnackBar(content: Text(state.message), backgroundColor: Colors.red),
                        );
                    }
                },
                builder: (context, state) {
                    return SingleChildScrollView(
                        padding: const EdgeInsets.all(16.0),
                        child: Form(
                            key: _formKey,
                            child: Column(
                                children: [
                                    TextFormField(
                                        controller: _nameController,
                                        decoration: const InputDecoration(labelText: "Name", border: OutlineInputBorder()),
                                        validator: (v) => (v == null || v.isEmpty) ? "Field is required" : null,
                                    ),
                                    const SizedBox(height: 16),
                                    TextFormField(
                                        controller: _priceController,
                                        decoration: const InputDecoration(labelText: "Price", border: OutlineInputBorder()),
                                        keyboardType: TextInputType.number,
                                        validator: (v) {
                                            if (v == null || v.isEmpty) return "Field is required";
                                            if (double.tryParse(v) == null) return "Must be a valid number";
                                            if (double.parse(v) < 0) return "The price cannot be negative";
                                            return null;
                                        },
                                    ),
                                    const SizedBox(height: 16),
                                    TextFormField(
                                        controller: _currencyController,
                                        decoration: const InputDecoration(labelText: "Currency ID (UUID)", border: OutlineInputBorder()),
                                        validator: (v) => (v == null || v.isEmpty) ? "Field is required" : null,
                                    ),
                                    const SizedBox(height: 30),
                                    
                                    SizedBox(
                                        width: double.infinity,
                                        height: 50,
                                        child: ElevatedButton(
                                            onPressed: state is CreateProductLoading 
                                                ? null
                                                : () {
                                                    if (_formKey.currentState!.validate()) {
                                                        context.read<CreateProductCubit>().addProduct(
                                                            _nameController.text,
                                                            double.tryParse(_priceController.text) ?? 0.0,
                                                            _currencyController.text,
                                                        );
                                                    }
                                                },
                                            child: state is CreateProductLoading
                                                ? const CircularProgressIndicator()
                                                : const Text("Save"),
                                        ),
                                    )
                                ],
                            ),
                        ),
                    );
                },
            ),
        );
    }
}
