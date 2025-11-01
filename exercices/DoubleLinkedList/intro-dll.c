#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct tnode {
    char* val;
    int val_length;

    struct tnode* previous;
    struct tnode* next;
} dll_node;

dll_node* new_node(char* v) {
    dll_node* new = malloc(sizeof(dll_node));

    new->val = v;
    new->val_length = strlen(v);

    new->next = NULL;    
    new->previous = NULL;    

    return new;
}

void free_node(dll_node* node) {
    free(node);
}

void print_node(dll_node node) {
    printf("%s", node.val);
    if (node.next != NULL) printf(" -> ");
}

void print_list(dll_node* head) {
    dll_node* current = head;
    while (current != NULL) {
        print_node(*current);
        current = current->next;
    }

    printf("\n");
}

void add_to_end(dll_node* head, dll_node* new_node) {
    dll_node* current = head;
    while (current->next != NULL) 
        current = current->next;

    current->next = new_node;
    new_node->previous = current;
}

void add_to_start(dll_node* head, dll_node* new_node) {
    new_node->next = head;
    head->previous = new_node;
}

void add_after_index(dll_node* head, dll_node* new_node, int index) {
    dll_node* current = head;
    int current_index = 1;

    while (current->next != NULL && current_index < index) {
        current = current->next;
        current_index++;
    } 

    new_node->next = current->next;
    new_node->previous = current;

    current->next = new_node;
    if (new_node->next != NULL) new_node->next->previous = new_node; 

}

dll_node* delete_at_index(dll_node* head, int index) {
    dll_node* current = head;
    int current_index = 0;

    // List empty
    if (current == NULL) return NULL;
    
    // Go to node
    while (current->next != NULL && current_index < index) {
        current = current->next;
        current_index++;
    } 
    
    // Get potential new head
    dll_node* newhead = NULL;
    if (current->previous == NULL) newhead = current->next;
    else newhead = head;

    // Remove node
    if (current->previous != NULL) current->previous->next = current->next;
    if (current->next != NULL) current->next->previous = current->previous;
    free_node(current);

    return newhead;
}

int find_index_of_node(dll_node* head, char* search) {
    dll_node* current = head;
    int current_index = 0;

    while (current != NULL) {
        if (strcmp(current->val, search) == 0) return current_index;

        current_index++;
        current = current->next;
    }

    return -1;
}

int main() {
    printf("== Print single node\n");
    dll_node* head = new_node("start");
    print_node(*head); 
    printf("\n");
    
    
    printf("== Add elements and print whole list\n");
    dll_node* node_a = new_node("a");
    dll_node* node_b = new_node("b");
    dll_node* node_c = new_node("c");
    dll_node* tail = new_node("end");
    
    add_to_end(head, node_a);
    add_to_end(head, node_b);
    add_to_end(head, node_c);
    add_to_end(head, tail);
    
    print_list(head);

    printf("== Add new head\n");
    dll_node* newhead = new_node("new start");
    add_to_start(head, newhead);
    
    print_list(newhead);
    
    printf("== Add element in middle\n");
    dll_node* between = new_node("in between");
    add_after_index(newhead, between, 3);
    
    print_list(newhead);

    printf("== Find element\n");
    printf("searching 'a' -> %d\n", find_index_of_node(newhead, "a"));
    printf("searching 'b' -> %d\n", find_index_of_node(newhead, "b"));
    printf("searching 'new start' -> %d\n", find_index_of_node(newhead, "new start"));
    printf("searching 'end' -> %d\n", find_index_of_node(newhead, "end"));
    printf("searching 'ababa' -> %d\n", find_index_of_node(newhead, "ababa"));

    
    printf("== Delete element in middle\n");
    newhead = delete_at_index(newhead, 0);
    newhead = delete_at_index(newhead, 3);

    print_list(newhead);



    return 0;
}